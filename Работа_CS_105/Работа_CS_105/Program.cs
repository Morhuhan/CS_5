using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Работа_CS_105
{
    internal class Program
    {
        // Иерархия интерфейсов
        interface IDraw
        {
            void Draw();
        }

        interface IDraw2 : IDraw
        {
            void DrawToPointer();
        }

        interface IDraw3 : IDraw2
        {
            void DrawToMetafile();
        }

        class SuperImage : IDraw3
        {
            public void Draw()
            {
                Console.WriteLine("IDraw Draw");
            }

            public void DrawToPointer()
            {
                Console.WriteLine("IDraw2 DrawToPointer");
            }

            public void DrawToMetafile()
            {
                Console.WriteLine("IDraw3 DrawToMetafile");
            }
        }
        //////////////////////////


        interface IDrawable
        {
            void Draw();
        }

        interface IPointy
        {
            int Points();
        }

        public interface IDraw3D
        {
            void Draw();
        }

        abstract class Shape : IDrawable
        {
            public string name = "figure";

            private int ID = 0;

            public Shape() {}

            public Shape(string name_)
            {
                name = name_;
            }

            // При помощи this вызывается другой конструктор этого же класса
            public Shape(string name_, int id) : this(name_) { ID = id; }

            // Абстрактный класс, наследующий интерфейс, не может иметь модификатор override
            public virtual void Draw() { }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
        }

        class Circle : Shape
        {
            public override void Draw()
            {
                Console.WriteLine("Circle " + name);
            }
        }

        class Square : Shape, IPointy, IDraw3D
        {
            public override void Draw()
            {
                Console.WriteLine("Square " + name);
            }

            // Переопределение Draw из IDrow3D
            void IDraw3D.Draw()
            {
                Console.WriteLine("Square 3D {0}", name);
            }

            // Реализация метода интерфейса должна иметь модификатор public
            public int Points() {
                return 4;
            }
        }

        public static void DrawShapeIn3D(IDraw3D itf3d)
        {
            Console.WriteLine("*** DrawShapeIn3D ***");
            itf3d.Draw();
        }


        static void Main(string[] args)
        {

            Square s = new Square();
            s.Draw();

            Circle c = new Circle();
            c.Draw();

            Shape fc = new Circle();
            fc.Draw();

            Shape fs = new Square();
            fs.Draw();

            Console.WriteLine("points = {0}", s.Points());

            IPointy ip;

            // Способ 1. Пробуем привести к типу интерфейса.
            try
            {
                ip = (IPointy)s;
                Console.WriteLine("points = {0}", ip.Points());

                ip = (IPointy)fs;
                Console.WriteLine("points = {0}", ip.Points());

                ip = (IPointy)c;
                Console.WriteLine("points = {0}", ip.Points());
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("No IPointy");
            }

            // Способ 2. Используем ключевое слово as.
            ip = s as IPointy;
            if (ip == null) { Console.WriteLine("No as IPointy"); }
            else { Console.WriteLine("Points = {0}", ip.Points()); }

            ip = fs as IPointy;
            if (ip == null) { Console.WriteLine("No as IPointy"); }
            else { Console.WriteLine("Points = {0}", ip.Points()); }

            ip = c as IPointy;
            if (ip == null) { Console.WriteLine("No as IPointy"); }
            else { Console.WriteLine("Points = {0}", ip.Points()); }

            // Способ 3. Используем ключевое слово is.
            if (s is IPointy) { Console.WriteLine("Points = {0}", ((IPointy)s).Points()); }
            else { Console.WriteLine("No is IPointy"); }

            if (fs is IPointy) { Console.WriteLine("Points = {0}", ((IPointy)s).Points()); }
            else { Console.WriteLine("No is IPointy"); }

            if (c is IPointy) { Console.WriteLine("Points = {0}", ((IPointy)s).Points()); }
            else { Console.WriteLine("No is IPointy"); }

            // Может возникнуть неопределенность, поскольку Square переопределяет Draw
            if (s is IDraw3D)
            {
                ((IDraw3D)s).Draw();

                // Интерфей как параметр метода
                DrawShapeIn3D((IDraw3D)s);
            }
            else
            {
                Console.WriteLine("No IDrow3D");
            }

            // Иерархия интерфейсов
            SuperImage si = new SuperImage();

            try
            {
                // Получаем ссылку на интерфейс IDraw
                IDraw idr = (IDraw)si;
                idr.Draw();

                // Получаем ссылку на интерфейс IDraw2
                IDraw2 idr2 = (IDraw2)si;
                idr2.Draw();
                idr2.DrawToPointer();

                // Не хотим использовать ссылку на интерфейс IDraw3
                ((IDraw3)si).Draw();
                ((IDraw3)si).DrawToPointer();
                ((IDraw3)si).DrawToMetafile();
            }
            catch { Console.WriteLine("No interface"); }
            
        }
    }
}
