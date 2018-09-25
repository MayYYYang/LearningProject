using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//  1.数据库必须有自增涨非空列 code first 约定的 貌似 ，只找到一句话 很模糊没找到官方文档
// 2. GameGrouping  :   IEntity IEntity  定义 的 类型需要是接口 否则 GetTypes 里面不好使
//3. EntityState 不加也好使

namespace ConsoleApplication1
{

    public interface IEntity
    {
    }
    [Table("iDoctor_Data_GameGrouping")]
    public  class GameGrouping : IEntity
    {
        public string ProjectHcpId { get; set; }
        public string GroupName { get; set; }
        public int? VersionNumber { get; set; }
        public int? OwnerId { get; set; }
        public int? ModifiedId { get; set; }
        public DateTime? CreatedUtc { get; set; }
        public DateTime? ModifiedUtc { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
    [Table("iDoctor_Data_GameGrouping1")]
    public class GameGrouping1 : IEntity
    {
        public string ProjectHcpId { get; set; }
        public string GroupName { get; set; }
        public int? VersionNumber { get; set; }
        public int? OwnerId { get; set; }
        public int? ModifiedId { get; set; }
        public DateTime? CreatedUtc { get; set; }
        public DateTime? ModifiedUtc { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
     public class GameGroupingService<GameGrouping> : DALBase<GameGrouping> where GameGrouping : class
    {

    }

    /// <summary>
    /// 定义泛型,必须规定T为类，不然db.Set<T>会报错
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public  class DALBase<T> where T : class
    {
        //因为我们要操作数据库，所以先实例化一个上下文
        Model1Container db = new Model1Container();

        public IQueryable<T> Entities { get { return db.Set<T>(); } }
        #region 添加方法
        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="Model"></param>
        public void add(T Model)
        {
            db.Set<T>().Add(Model);
            //db.Entry<T>(Model).State = System.Data.EntityState.Added;
            db.SaveChanges();
        }
        #endregion

        #region 修改单个实体
        /// <summary>
        /// 修改单个实体    ,根据不同 属性更改
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="strs"></param>
        public void update(T Model, params string[] strs)
        {
            DbEntityEntry entry = db.Entry<T>(Model);
            entry.State = System.Data.EntityState.Modified;
            foreach (string tempStr in strs)
            {
                entry.Property(tempStr).IsModified = true;
            }
            db.SaveChanges();
        }
        #endregion

        #region 批量修改，根据反射，稍微要复杂一些
        /// <summary>
        /// 批量修改，根据反射，稍微要复杂一些
        /// </summary>
        /// <param name="Model">将值存入属性中</param>
        /// <param name="where">批量修改的条件</param>
        /// <param name="strs">属性</param>
        public void updateBatch(T Model, Expression<Func<T, bool>> where, params string[] strs)
        {
            //先根据条件查出符合要修改的集合
            List<T> tempList = db.Set<T>().Where(where).ToList();
            //获取类型
            Type t = typeof(T);
            //利用反射获取T类型public属性集合
            List<PropertyInfo> tempPro = t.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).ToList();
            Dictionary<string, PropertyInfo> propertyDic = new Dictionary<string, PropertyInfo>();
            //遍历T的所有属性，将符合修改的存入字典中
            tempPro.ForEach(p => { if (strs.Contains(p.Name)) { propertyDic.Add(p.Name, p); } });
            //遍历要修改的属性
            foreach (string str in strs)
            {
                if (propertyDic.ContainsKey(str))
                {
                    PropertyInfo propertyInfo = propertyDic[str];
                    //获取要修改属性的值
                    object value = propertyInfo.GetValue(Model, null);
                    foreach (T tempData in tempList)
                    {
                        //设置值
                        propertyInfo.SetValue(tempData, value, null);
                    }
                }
            }
        }
        #endregion

        #region 根据实体id删除操作
        /// <summary>
        /// 根据实体id删除操作
        /// </summary>
        /// <param name="Model"></param>
        public void remove(T Model)
        {
            db.Set<T>().Attach(Model);
            db.Set<T>().Remove(Model);
            db.SaveChanges();
        }
        #endregion

        #region 根据条件删除操作
        /// <summary>
        /// 根据条件删除操作
        /// </summary>
        /// <param name="remWhere"></param>
        public void removeByWhere(Expression<Func<T, bool>> remWhere)
        {
            List<T> tempList = db.Set<T>().Where(remWhere).ToList();
            tempList.ForEach(t => { db.Set<T>().Attach(t); db.Set<T>().Remove(t); });
            db.SaveChanges();
        }
        #endregion

        #region 一般带条件查询
        /// <summary>
        /// 一般带条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> getList(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Where(where).ToList();
        }
        #endregion

        #region 带条件排序，页码页容量查询
        /// <summary>
        /// 带条件排序，页码页容量查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<T> getListOrder<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy, int pageSize, int pageIndex)
        {
            return db.Set<T>().Where(where).OrderBy(orderBy).Skip(pageIndex - 1).Take(pageSize).ToList();
        }
        #endregion
    }

    public partial class Model1Container : DbContext

    {

        public Model1Container()

            : base("name=ConnectionString")  
        {

        }    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)

        {
            MapperExtender extender = new MapperExtender();
            extender.GetTypes(modelBuilder.Configurations);
            base.OnModelCreating(modelBuilder);               
        }     
    }


    public class MapperExtender
    {
        public void GetTypes(ConfigurationRegistrar configurationBuilder)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IEntity))))
                .ToList();
            foreach (var t in types)
            {

                var Types = typeof(EntityConfigurationAll<,>);  
                var ins = Types.MakeGenericType(t, typeof(Int32));

                var Config = Activator.CreateInstance(ins) as IEntityMapper;
                Config.RegistTo(configurationBuilder);
            }
        }

    }

    public abstract class EntityConfigurationBase<TEntity, TKey> : EntityTypeConfiguration<TEntity>, IEntityMapper
     where TEntity : class
    {
        /// <summary>
        /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
        /// </summary>
        /// <param name="configurations">实体映射配置注册器</param>
        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }

    public class EntityConfigurationAll<TEntity, TKey> : EntityConfigurationBase<TEntity, TKey>
      where TEntity : class
    {

    }
    public interface IEntityMapper
    {
        /// <summary>
        /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
        /// </summary>
        /// <param name="configurations">实体映射配置注册器</param>
        void RegistTo(ConfigurationRegistrar configurations);
    }

    public class DBMain
    {
        public void Main()
        {
            GameGroupingService<GameGrouping> test = new GameGroupingService<GameGrouping>();
            test.add(new GameGrouping { Id = 7898, GroupName = "dddddddddas", ProjectHcpId = "ddddddddd" });
            DALBase<GameGrouping1> test1 = new DALBase<GameGrouping1>();
            var list = test1.getList(a => true).ToList();

            list[0].VersionNumber = 898;
            test1.update(list[0], "VersionNumber");
            test1.remove(list[0]);

            // 不to List 时无法进行jion , 
            var set = from n in test.Entities.ToList()
                      join m in test1.Entities.ToList() on n.Id equals m.Id
                      select m;
            var dd = set.ToList();
        }
    }
}