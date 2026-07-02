# Store Management System — เอกสารโปรเจกต์

> ระบบจัดการสต๊อกสินค้าในคลัง (Windows Forms / C# / .NET Framework 4.7.2)
> ฐานข้อมูล: SQL Server LocalDB (`StoreManagementDB`, สร้างอัตโนมัติตอนรันครั้งแรก ไม่ผูก path เครื่องใดเครื่องหนึ่ง)

---

## 1. ภาพรวมสถาปัตยกรรม (Architecture)

```
Program.cs  (จุดเริ่มโปรแกรม)
   │  เรียก Database.EnsureSchema()  → สร้างตารางถ้ายังไม่มี
   ▼
Form1  (หน้า Login) ───► RegisterForm (หน้าสมัครสมาชิก)
   │  ล็อกอินสำเร็จ
   ▼
MainForm  (โครงหลัก: แถบบน + เมนูซ้าย + พื้นที่เนื้อหา panel3)
   │  โหลด UserControl เข้า panel3 ตามเมนูที่กด
   ├── Dashboard      (สรุปภาพรวม)
   ├── Products       (จัดการสินค้า)
   ├── Categories     (จัดการหมวดหมู่)
   ├── StockManage    (เพิ่ม/ลด สต๊อก)
   └── StockLog       (ประวัติการเคลื่อนไหว)
```

### ไฟล์สำคัญ
| ไฟล์ | หน้าที่ |
|------|---------|
| `Program.cs` | จุดเริ่มโปรแกรม + เตรียมฐานข้อมูล |
| `Database.cs` | **ศูนย์กลาง** connection string + `EnsureSchema()` สร้างตาราง + `Session.CurrentUser` |
| `PasswordHasher.cs` | Hash/verify รหัสผ่านแบบ salted PBKDF2 (100,000 iterations) |
| `Form1.cs` | หน้า Login |
| `RegisterForm.cs` | หน้าสมัครสมาชิก |
| `MainForm.cs` | โครงหลัก + ระบบนำทาง (navigation) |
| `Dashboard.cs` | การ์ดสรุป + ตารางความเคลื่อนไหวล่าสุด |
| `Products.cs` | CRUD สินค้า |
| `Categories.cs` | CRUD หมวดหมู่ |
| `StockManage.cs` | บันทึกการรับเข้า/เบิกออก |
| `StockLog.cs` | ดูประวัติ + กรองข้อมูล |

---

## 2. โครงสร้างฐานข้อมูล (Database Schema)

ตารางทั้งหมดถูกสร้างอัตโนมัติโดย `Database.EnsureSchema()` เมื่อเปิดโปรแกรมครั้งแรก

### `users` — ผู้ใช้งาน
| คอลัมน์ | ชนิด | หมายเหตุ |
|---------|------|----------|
| id | INT (PK, identity) | |
| username | VARCHAR | |
| password | VARCHAR | เก็บเป็น salted hash (PBKDF2) ผ่าน `PasswordHasher` — ไม่ใช่ plain text |
| date_register | DATE | |

### `categories` — หมวดหมู่สินค้า
| คอลัมน์ | ชนิด | หมายเหตุ |
|---------|------|----------|
| id | INT (PK, identity) | |
| name | NVARCHAR(200) | ชื่อหมวดหมู่ |
| description | NVARCHAR(MAX) | คำอธิบาย |
| date_created | DATETIME | ค่าเริ่มต้น = วันที่ปัจจุบัน |

### `products` — สินค้า
| คอลัมน์ | ชนิด | หมายเหตุ |
|---------|------|----------|
| id | INT (PK, identity) | |
| product_code | NVARCHAR(100) | รหัสสินค้า |
| name | NVARCHAR(200) | ชื่อสินค้า |
| category_id | INT (FK → categories.id) | หมวดหมู่ (ว่างได้) |
| brand | NVARCHAR(200) | ยี่ห้อ |
| description | NVARCHAR(MAX) | รายละเอียด |
| quantity | INT | **จำนวนคงเหลือปัจจุบัน** |
| date_created | DATETIME | |

### `stock_movements` — ประวัติการเคลื่อนไหวสต๊อก
| คอลัมน์ | ชนิด | หมายเหตุ |
|---------|------|----------|
| id | INT (PK, identity) | |
| product_id | INT (FK → products.id) | สินค้า |
| movement_type | NVARCHAR(10) | `IN` (รับเข้า) หรือ `OUT` (เบิกออก) |
| quantity | INT | จำนวนที่เคลื่อนไหว |
| department | NVARCHAR(200) | แผนกที่นำไปใช้ (เฉพาะ OUT) |
| used_for | NVARCHAR(MAX) | นำไปใช้ที่ไหน/ทำอะไร (เฉพาะ OUT) |
| note | NVARCHAR(MAX) | หมายเหตุ |
| moved_by | NVARCHAR(200) | ผู้บันทึก (= ผู้ที่ล็อกอิน) |
| date_moved | DATETIME | เวลาที่บันทึก |

**ความสัมพันธ์:**
`categories 1 ──< products 1 ──< stock_movements`
(ลบหมวดหมู่/สินค้าที่ถูกอ้างอิงอยู่ไม่ได้ — ระบบจะเตือน)

---

## 3. Flow การทำงานแต่ละเมนู

### 3.1 Login / Register
1. เปิดโปรแกรม → หน้า **Login** (`Form1`)
2. กรอก username/password → กด **Login**
   - ถูกต้อง → เข้า `MainForm` พร้อมส่งชื่อผู้ใช้ไปเก็บใน `Session.CurrentUser`
   - ผิด → แจ้งเตือน (ไม่เข้าระบบ)
3. ยังไม่มีบัญชี → กด **Sign up** → หน้า `RegisterForm` → สมัคร → กลับมา Login

### 3.2 Dashboard (ภาพรวม)
- โหลดอัตโนมัติเมื่อเข้า `MainForm`
- แสดงการ์ด 3 ใบ:
  - **Total Products** = จำนวนสินค้า (`COUNT products`)
  - **Total Category** = จำนวนหมวดหมู่ (`COUNT categories`)
  - **Total Stocks** = ผลรวมจำนวนคงเหลือทั้งหมด (`SUM products.quantity`)
- ตารางด้านล่าง = 20 รายการเคลื่อนไหวล่าสุด

### 3.3 Product (จัดการสินค้า)
**การไหล:** เลือกแถวในตาราง → ฟอร์มด้านล่างเติมค่าให้อัตโนมัติ → แก้ไข → กดปุ่ม

| ปุ่ม | การทำงาน |
|------|----------|
| **Add** | เพิ่มสินค้าใหม่ (ต้องมีชื่อสินค้า) |
| **Update** | แก้สินค้าที่เลือกจากตาราง |
| **Delete** | ลบสินค้า (ลบไม่ได้ถ้ามีประวัติสต๊อกแล้ว) |
| **Clear** | ล้างฟอร์ม |

- **Category** เป็น dropdown ที่ดึงจากตาราง `categories` แบบ real-time (มีตัวเลือก `-- None --`)
- **Initial Quantity** = จำนวนตั้งต้นตอนสร้างสินค้า (หลังจากนั้นควรปรับผ่านเมนู Stock)
- ช่อง **Search** = ค้นหาจากชื่อ/รหัส/ยี่ห้อ ทันทีที่พิมพ์

### 3.4 Category (จัดการหมวดหมู่)
โครงเหมือน Product แต่ฟิลด์แค่ **ชื่อ** + **คำอธิบาย**

| ปุ่ม | การทำงาน |
|------|----------|
| **Add** | เพิ่มหมวดหมู่ |
| **Update** | แก้หมวดหมู่ที่เลือก |
| **Delete** | ลบ (ลบไม่ได้ถ้ามีสินค้าใช้หมวดหมู่นี้อยู่) |
| **Clear** | ล้างฟอร์ม |

### 3.5 Update Stock (เพิ่ม/ลด สต๊อก) — หัวใจของระบบ
**การไหล:**
1. เลือก **Product** จาก dropdown → ระบบโชว์ **Current Stock** (จำนวนคงเหลือ)
2. เลือกชนิด:
   - **IN (เพิ่ม/รับเข้า)** → รับสินค้าเข้าคลัง
   - **OUT (ลด/นำไปใช้)** → เบิกออก (ช่อง Department + Used For จะเปิดให้กรอก)
3. กรอก **Quantity**, และถ้าเป็น OUT ต้องระบุ **Department** (แผนก) + **Used For** (นำไปใช้ที่ไหน)
4. กด **Save Movement**

**สิ่งที่เกิดขึ้นเบื้องหลัง (ใน transaction เดียว):**
- อ่านจำนวนคงเหลือปัจจุบัน
- ถ้า OUT แล้วจำนวนไม่พอ → ยกเลิก + เตือน
- อัปเดต `products.quantity` (`+` ถ้า IN, `-` ถ้า OUT)
- บันทึกลง `stock_movements` (พร้อม แผนก/ที่ใช้/ผู้บันทึก/เวลา)

> ใช้ **SqlTransaction** เพื่อกันข้อมูลไม่ตรงกัน — ถ้าขั้นใดพลาด rollback ทั้งหมด

ตารางล่าง = 50 รายการล่าสุด (อัปเดตทันทีหลังบันทึก)

### 3.6 Update Log (ประวัติการเคลื่อนไหว)
- แสดงทุกรายการใน `stock_movements` (join ชื่อสินค้า)
- **ตัวกรอง:**
  - Search (ชื่อสินค้า / แผนก / ที่ใช้ / ผู้บันทึก)
  - Type (All / IN / OUT)
  - ช่วงวันที่ From – To
- แถบสรุปด้านบน: จำนวนรายการ + ยอดรวม IN + ยอดรวม OUT

### 3.7 Sign out
กดปุ่ม signout มุมล่างซ้าย → ยืนยัน → กลับหน้า Login

---

## 4. วิธี Build & Run

โปรเจกต์เป็น **.NET Framework 4.7.2** → ต้องใช้ **MSBuild ของ Visual Studio** (ใช้ `dotnet build` ไม่ได้)

```
"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" ^
    StoreManagementSystem\StoreManagementSystem.csproj /t:Rebuild /p:Configuration=Debug
```

หรือเปิดใน Visual Studio แล้วกด **F5** / **Rebuild Solution**

ผลลัพธ์: `StoreManagementSystem\bin\Debug\StoreManagementSystem.exe`

---

## 5. จุดที่ควรปรับปรุง / ข้อกังวล

### ✅ แก้แล้ว
- **รหัสผ่าน** — เดิมเก็บเป็น plain text ตอนนี้ hash ด้วย salted PBKDF2 ผ่าน `PasswordHasher.cs` แล้ว (ทั้งตอน login และ register)

### 🟡 ควรมีในระบบสต๊อกจริง
1. **แจ้งเตือนของใกล้หมด (low stock)** — เพิ่มฟิลด์ `min_quantity` ในสินค้า แล้วไฮไลต์/เตือนเมื่อ `quantity <= min_quantity`
2. **แก้ไข/ลบรายการใน Log ไม่ได้** — ตอนนี้ประวัติแก้ไม่ได้เลย (ดีต่อ audit) แต่ถ้าบันทึกผิดจะแก้ไม่ได้ → อาจเพิ่มระบบ "ยกเลิกรายการ" (สร้าง movement ย้อนกลับ) แทนการลบตรงๆ
3. **Export รายงาน** เป็น Excel/CSV/PDF จากหน้า Log

### 🟢 คุณภาพโค้ด / ประสบการณ์ใช้งาน
4. **ไฟล์ Designer ห้ามเปิดแก้พร้อมกับที่มีการแก้บนดิสก์** — Visual Studio ที่เปิดค้างอาจเขียนทับ (เคยเกิดในโปรเจกต์นี้แล้ว) → แก้ทีละที่แล้ว Save ทันที
5. **ยังไม่มีระบบสิทธิ์ (role)** — ทุกคนที่ล็อกอินทำได้ทุกอย่าง → ถ้าจะใช้จริงหลายคน ควรมี admin / staff
6. **Soft delete** — ปัจจุบันลบสินค้า/หมวดหมู่ที่มีการอ้างอิงไม่ได้เลย ทางเลือกที่ดีกว่าคือเพิ่มฟิลด์ `is_active` แล้วซ่อนแทนการลบ

### สรุป
โครงสร้างหลักครบและใช้งานได้จริงแล้ว (CRUD + สต๊อกแบบมี transaction + ประวัติ + รหัสผ่านถูก hash)
เรื่องที่เหลือเป็น feature เสริม (low stock, export, role, soft delete) ไม่กระทบความปลอดภัยพื้นฐานแล้ว
