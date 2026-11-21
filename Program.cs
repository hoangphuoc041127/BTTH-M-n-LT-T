using System;
using System.Text;

namespace ConsoleApp26
{
    // ===================== BÀI 1: DIEM =====================
    class Diem
    {
        private double x;   // Hoành độ
        private double y;   // Tung độ

        public Diem() { this.x = 0; this.y = 0; }
        public Diem(double x, double y) { this.x = x; this.y = y; }

        public void Nhap()
        {
            Console.Write("  Nhập hoành độ x: ");
            while (!double.TryParse(Console.ReadLine(), out x)) Console.Write("  Nhập lại x: ");
            Console.Write("  Nhập tung độ y: ");
            while (!double.TryParse(Console.ReadLine(), out y)) Console.Write("  Nhập lại y: ");
        }

        public void HienThi() { Console.Write("({0}, {1})", x, y); }

        public double KhoangCach(Diem d)
        {
            double dx = x - d.x, dy = y - d.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    // ===================== BÀI 2: EMPLOYEE =====================
    class Employee
    {
        private static int dem = 0;            // bộ đếm sinh id tự động
        private static double basicSalary = 1; // lương cơ bản dùng chung

        private int id;
        private string name;
        private readonly int yearOfBirth;      // không đổi sau khi tạo
        private double salaryLevel;

        public Employee(string name, int yearOfBirth, double salaryLevel)
        {
            this.id = ++dem;
            this.name = name;
            this.yearOfBirth = yearOfBirth;
            this.salaryLevel = salaryLevel;
        }

        public int GetId() { return id; }
        public string GetName() { return name; }
        public int GetYearOfBirth() { return yearOfBirth; }
        public double GetIncome() { return salaryLevel * basicSalary; }

        public void SetSalaryLevel(double level) { salaryLevel = level; }
        public static void SetBasicSalary(double salary) { basicSalary = salary; }

        public void Display()
        {
            Console.WriteLine("ID: {0} | Họ tên: {1} | Năm sinh: {2} | Lương cơ bản: {3} | Thu nhập: {4}",
                id, name, yearOfBirth, basicSalary, GetIncome());
        }
    }

    // ===================== BÀI 3: STACK =====================
    class Stack
    {
        private int top;
        private int[] s;

        public Stack() : this(20) { }
        public Stack(int n) { s = new int[n]; top = -1; }

        public bool IsEmpty() { return top == -1; }
        public bool IsFull() { return top == s.Length - 1; }

        public void Push(int x)
        {
            if (IsFull()) throw new InvalidOperationException("Stack đầy!");
            s[++top] = x;
        }

        public int Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack rỗng!");
            return s[top--];
        }
    }

    // ===================== BÀI 4: COMPLEXNUMBER =====================
    class ComplexNumber
    {
        private double thuc;   // phần thực
        private double ao;     // phần ảo

        public ComplexNumber(double thuc, double ao) { this.thuc = thuc; this.ao = ao; }

        public void HienThi()
        {
            if (ao >= 0) Console.Write("{0} + {1}i", thuc, ao);
            else Console.Write("{0} - {1}i", thuc, -ao);
        }

        public ComplexNumber Tong(ComplexNumber t2) { return new ComplexNumber(thuc + t2.thuc, ao + t2.ao); }
        public ComplexNumber Hieu(ComplexNumber t2) { return new ComplexNumber(thuc - t2.thuc, ao - t2.ao); }
        // (a+bi)(c+di) = (ac-bd) + (bc+ad)i
        public ComplexNumber Tich(ComplexNumber t2)
        {
            return new ComplexNumber(thuc * t2.thuc - ao * t2.ao, ao * t2.thuc + thuc * t2.ao);
        }
    }

    // ===================== BÀI 5: MATRAN =====================
    class MaTran
    {
        private int sh, sc;
        private double[,] a;

        public MaTran(int sh, int sc)
        {
            this.sh = sh; this.sc = sc;
            a = new double[sh, sc];
        }

        public void Nhap()
        {
            for (int i = 0; i < sh; i++)
                for (int j = 0; j < sc; j++)
                {
                    Console.Write("  a[{0},{1}] = ", i, j);
                    while (!double.TryParse(Console.ReadLine(), out a[i, j])) Console.Write("  Nhập lại: ");
                }
        }

        public void Print()
        {
            for (int i = 0; i < sh; i++)
            {
                for (int j = 0; j < sc; j++) Console.Write("{0,8:0.##}", a[i, j]);
                Console.WriteLine();
            }
        }

        // hai ma trận tương thích (cùng cấp) -> có thể cộng/trừ
        public bool TuongThich(MaTran m) { return sh == m.sh && sc == m.sc; }
        // tương thích để nhân: số cột của A = số hàng của B
        public bool TuongThichNhan(MaTran m) { return sc == m.sh; }
        public bool MaTranVuong() { return sh == sc; }

        public MaTran Cong(MaTran m)
        {
            if (!TuongThich(m)) throw new InvalidOperationException("Hai ma trận không cùng cấp!");
            MaTran k = new MaTran(sh, sc);
            for (int i = 0; i < sh; i++)
                for (int j = 0; j < sc; j++) k.a[i, j] = a[i, j] + m.a[i, j];
            return k;
        }

        public MaTran Hieu(MaTran m)
        {
            if (!TuongThich(m)) throw new InvalidOperationException("Hai ma trận không cùng cấp!");
            MaTran k = new MaTran(sh, sc);
            for (int i = 0; i < sh; i++)
                for (int j = 0; j < sc; j++) k.a[i, j] = a[i, j] - m.a[i, j];
            return k;
        }

        public void DoiDau()
        {
            for (int i = 0; i < sh; i++)
                for (int j = 0; j < sc; j++) a[i, j] = -a[i, j];
        }

        public MaTran ChuyenVi()
        {
            MaTran k = new MaTran(sc, sh);
            for (int i = 0; i < sh; i++)
                for (int j = 0; j < sc; j++) k.a[j, i] = a[i, j];
            return k;
        }

        public MaTran Tich(MaTran m)
        {
            if (!TuongThichNhan(m)) throw new InvalidOperationException("Số cột A phải bằng số hàng B!");
            MaTran k = new MaTran(sh, m.sc);
            for (int i = 0; i < sh; i++)
                for (int j = 0; j < m.sc; j++)
                {
                    double t = 0;
                    for (int l = 0; l < sc; l++) t += a[i, l] * m.a[l, j];
                    k.a[i, j] = t;
                }
            return k;
        }
    }

    // ===================== CHƯƠNG TRÌNH CHÍNH =====================
    class Program
    {
        static double DocSo(string nhan)
        {
            double v;
            Console.Write(nhan);
            while (!double.TryParse(Console.ReadLine(), out v)) Console.Write("  Nhập lại: ");
            return v;
        }

        static int DocInt(string nhan)
        {
            int v;
            Console.Write(nhan);
            while (!int.TryParse(Console.ReadLine(), out v)) Console.Write("  Nhập lại: ");
            return v;
        }

        static void Bai1()
        {
            Diem o = new Diem();
            Console.Write("Điểm gốc: "); o.HienThi(); Console.WriteLine();
            Diem p = new Diem(3, 4);
            Console.Write("Điểm P: "); p.HienThi(); Console.WriteLine();
            Diem q = new Diem();
            Console.WriteLine("Nhập điểm Q:"); q.Nhap();
            Console.Write("Điểm Q: "); q.HienThi(); Console.WriteLine();
            Console.Write("Khoảng cách P - Q = {0:0.####}\n", p.KhoangCach(q));
        }

        static void Bai2()
        {
            Employee.SetBasicSalary(1500000);
            Employee e1 = new Employee("Nguyen Van A", 1995, 2.34);
            Employee e2 = new Employee("Tran Thi B", 1998, 3.0);
            e1.Display();
            e2.Display();

            Console.WriteLine("\nSau khi đặt bậc lương e1 = 4.0 và lương cơ bản = 1800000:");
            e1.SetSalaryLevel(4.0);
            Employee.SetBasicSalary(1800000);
            e1.Display();
            e2.Display();
        }

        static void Bai3()
        {
            int n = DocInt("Nhập số phần tử tối đa của stack (0 = mặc định 20): ");
            Stack st = n > 0 ? new Stack(n) : new Stack();
            int chon;
            do
            {
                Console.WriteLine("\n1. Push  2. Pop  3. Kiểm tra rỗng  4. Kiểm tra đầy  0. Thoát");
                chon = DocInt("Chọn: ");
                try
                {
                    switch (chon)
                    {
                        case 1: int x = DocInt("Giá trị: "); st.Push(x); Console.WriteLine("Đã thêm {0}", x); break;
                        case 2: Console.WriteLine("Lấy ra: {0}", st.Pop()); break;
                        case 3: Console.WriteLine(st.IsEmpty() ? "Stack rỗng" : "Stack không rỗng"); break;
                        case 4: Console.WriteLine(st.IsFull() ? "Stack đầy" : "Stack chưa đầy"); break;
                    }
                }
                catch (Exception e) { Console.WriteLine("Lỗi: " + e.Message); }
            } while (chon != 0);
        }

        static void Bai4()
        {
            Console.WriteLine("Số phức thứ nhất:");
            ComplexNumber z1 = new ComplexNumber(DocSo("  Phần thực: "), DocSo("  Phần ảo: "));
            Console.WriteLine("Số phức thứ hai:");
            ComplexNumber z2 = new ComplexNumber(DocSo("  Phần thực: "), DocSo("  Phần ảo: "));

            Console.Write("z1 = "); z1.HienThi(); Console.WriteLine();
            Console.Write("z2 = "); z2.HienThi(); Console.WriteLine();
            Console.Write("z1 + z2 = "); z1.Tong(z2).HienThi(); Console.WriteLine();
            Console.Write("z1 - z2 = "); z1.Hieu(z2).HienThi(); Console.WriteLine();
            Console.Write("z1 * z2 = "); z1.Tich(z2).HienThi(); Console.WriteLine();
        }

        static MaTran NhapMaTran(string ten)
        {
            int h = DocInt("Số hàng của " + ten + ": ");
            int c = DocInt("Số cột của " + ten + ": ");
            MaTran m = new MaTran(h, c);
            Console.WriteLine("Nhập " + ten + ":");
            m.Nhap();
            return m;
        }

        static void Bai5()
        {
            MaTran A = NhapMaTran("A");
            MaTran B = NhapMaTran("B");
            Console.WriteLine("\nMa trận A:"); A.Print();
            Console.WriteLine("Ma trận B:"); B.Print();
            Console.WriteLine("A là ma trận vuông? {0}", A.MaTranVuong() ? "Có" : "Không");
            Console.WriteLine("A, B cùng cấp (cộng/trừ được)? {0}", A.TuongThich(B) ? "Có" : "Không");

            try { Console.WriteLine("\nA + B:"); A.Cong(B).Print(); }
            catch (Exception e) { Console.WriteLine("Lỗi: " + e.Message); }
            try { Console.WriteLine("A - B:"); A.Hieu(B).Print(); }
            catch (Exception e) { Console.WriteLine("Lỗi: " + e.Message); }
            try { Console.WriteLine("A * B:"); A.Tich(B).Print(); }
            catch (Exception e) { Console.WriteLine("Lỗi: " + e.Message); }

            Console.WriteLine("Chuyển vị của A:"); A.ChuyenVi().Print();
            A.DoiDau();
            Console.WriteLine("A sau khi đổi dấu:"); A.Print();
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            int chon;
            do
            {
                Console.WriteLine("\n===== BÀI THỰC HÀNH 1 =====");
                Console.WriteLine("1. Lớp Diem");
                Console.WriteLine("2. Lớp Employee");
                Console.WriteLine("3. Lớp Stack");
                Console.WriteLine("4. Lớp ComplexNumber");
                Console.WriteLine("5. Lớp MaTran");
                Console.WriteLine("0. Thoát");
                chon = DocInt("Chọn bài: ");
                Console.WriteLine();
                switch (chon)
                {
                    case 1: Bai1(); break;
                    case 2: Bai2(); break;
                    case 3: Bai3(); break;
                    case 4: Bai4(); break;
                    case 5: Bai5(); break;
                }
            } while (chon != 0);
        }
    }
}
