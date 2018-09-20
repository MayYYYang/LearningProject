using DesignPattern.Patterns.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  DesignPattern.Patterns.DB;
using System.Reflection;
using ConsoleApplication1;

namespace DesignPattern
{
    class Program
    {
        class ReflectionTest
        {
            //泛型类MyGenericClass有个静态函数DisplayNestedType
            public class MyGenericClass<T>
            {
                public static void DisplayNestedType()
                {
                    Console.WriteLine(typeof(T).ToString());
                }
            }

            public void DisplayType<T>()
            {
                Console.WriteLine(typeof(T).ToString());
            }
        }
        static void Main(string[] args)
        {

            //ReflectionTest rt = new ReflectionTest();

            //MethodInfo mi = rt.GetType().GetMethod("DisplayType");//先获取到DisplayType<T>的MethodInfo反射对象
            //mi.MakeGenericMethod(new Type[] { typeof(string) }).Invoke(rt, null);//然后使用MethodInfo反射对象调用ReflectionTest类的DisplayType<T>方法，这时要使用MethodInfo的MakeGenericMethod函数指定函数DisplayType<T>的泛型类型T

            //Type myGenericClassType = rt.GetType().GetNestedType("MyGenericClass`1");//这里获取MyGenericClass<T>的Type对象，注意GetNestedType方法的参数要用MyGenericClass`1这种格式才能获得MyGenericClass<T>的Type对象
            //myGenericClassType.MakeGenericType(new Type[] { typeof(float) }).GetMethod("DisplayNestedType", BindingFlags.Static | BindingFlags.Public).Invoke(null, null);
            ////然后用Type对象的MakeGenericType函数为泛型类MyGenericClass<T>指定泛型T的类型，比如上面我们就用MakeGenericType函数将MyGenericClass<T>指定为了MyGenericClass<float>，然后继续用反射调用MyGenericClass<T>的DisplayNestedType静态方法

            //Console.ReadLine();

            GameGroupingService<GameGrouping> test = new GameGroupingService<GameGrouping>();
            test.add(new GameGrouping { Id = 7898, GroupName ="dddddddddas",ProjectHcpId="ddddddddd"});
            DALBase<GameGrouping1> test1 = new DALBase<GameGrouping1>();
            test1.add(new GameGrouping1 { Id = 7898, GroupName = "dddddddddas", ProjectHcpId = "ddddddddd" });
            var list =test1.getList(a=>true).ToList();
            list[0].VersionNumber = 898;
            test1.update(list[0], "VersionNumber");
            test1.remove(list[0]);
            //DBMainFunction db = new DBMainFunction();
            //db.main();


        }
    }
}
