using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;      
using System.Data.Entity;
using System.Data;
using System.Linq.Expressions;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Transactions;
using System.Reflection;

namespace DesignPattern.Patterns.DB
{
    public interface IEntity 
    {
    }
    //[Table("Product")]
    //public class Product : EntityBase<int>
    //{
    //    public int Id { get; set; }
    //}   
     public class DBContext : DbContext
    {
        public DBContext(string con):base(con)
        {

        }
        //public DbSet<Product> Entity { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Product>());

            //MapperExtender extender = new MapperExtender();
            //extender.GetTypes(modelBuilder.Configurations);
            base.OnModelCreating(modelBuilder);         
        }

    }


    public interface IEntityMapper
    {
        /// <summary>
        /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
        /// </summary>
        /// <param name="configurations">实体映射配置注册器</param>
        void RegistTo(ConfigurationRegistrar configurations);
    }
    public interface IBaseDataService<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        TEntity Add(TEntity entity);
    }
    public abstract class EntityBase<TKey> : IEntity
    {
    }
        public class BaseDataService<TEntity,TKey> : IBaseDataService<TEntity,TKey> where TEntity : EntityBase<TKey>         
    {       
        static DBContext db1;        
        private readonly DbSet<TEntity> _dbSet;
        public BaseDataService()
        {              
            db1 = new DBContext("ConnectionString");
            _dbSet = db1.Set<TEntity>();    
        }
        public IQueryable<TEntity> Entities { get { return _dbSet; } }

        public List<TEntity> Fun()
        {
            var test= _dbSet.ToList();
           //var List1 = db1.Set().ToList();
            var List=1;
            return test;

        }
        public int update(TEntity entity)
        {
            
            db1.Set<TEntity>().Attach(entity);
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.GetValue(entity, null).ToString() == " ")
                        db1.Entry(entity).Property(prop.Name).CurrentValue = null;
                    db1.Entry(entity).Property(prop.Name).IsModified = true;
                }
            }
            return db1.SaveChanges();
        }
        public TEntity AddEntity(TEntity entity)
        {
            //EF4.0的写法  
            //添加实体
            //db.CreateObjectSet<T>().AddObject(entity);
            //EF5.0的写法
            //db1.ad
            ////下面的写法统一
            //db.SaveChanges();
            return entity;
        }

        public TEntity Add(TEntity entity)
        {

            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                db1.Set<TEntity>().Add(entity);
                int Count = db1.SaveChanges();
                Ts.Complete();
                return entity;
            }

            //var test = _dbSet.Add(entity);

            //db1.SaveChanges();
            ////var test= _dbSet.Attach(entity);
            ////SaveChanges();
            ////db1.Set<TEntity>().Add(entity);
            //// int Count = db1.SaveChanges();
            //return entity;
        }

        private void SaveChanges()
        {
            db1.SaveChanges();
        }
    }
  
    public class DBMainFunction
    {
        public void main()
        {
            //ProductService mb = new ProductService();
            //var test = mb.Fun();
            //test[0].Id = 9999;
            //mb.update(test[0]);
            //mb.Add(new Product { Id=999});
           

            //var test=mb.Fun();
        }
    }

    public interface IProductService
    {
        
    }
    public class ProductService: /*BaseDataService<Product, int>,*/ IProductService
    {

    }

    //public class MapperExtender {
    //    public void GetTypes(ConfigurationRegistrar configurationBuilder)
    //    {
    //        var types = AppDomain.CurrentDomain.GetAssemblies()
    //            .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(EntityBase<int>))))
    //            .ToList();
    //        foreach(var t in types)
    //        {

    //            var Types = typeof(EntityConfigurationAll<,>);
    //            // Type[] typeArgs = { startUpTaskType,typeof(Int32) };

    //            var ins = Types.MakeGenericType(t, typeof(Int32));

    //            var Config = Activator.CreateInstance(ins) as IEntityMapper;
    //            Config.RegistTo(configurationBuilder);  
    //        }
    //    }

    //}

    //public abstract class EntityConfigurationBase<TEntity, TKey> : EntityTypeConfiguration<TEntity>, IEntityMapper
    // where TEntity : class
    //{
    //    /// <summary>
    //    /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
    //    /// </summary>
    //    /// <param name="configurations">实体映射配置注册器</param>
    //    public void RegistTo(ConfigurationRegistrar configurations)
    //    {
    //        configurations.Add(this);
    //    }
    //}

    //public class EntityConfigurationAll<TEntity, TKey> : EntityConfigurationBase<TEntity, TKey>
    //  where TEntity : class
    //{

    //}
}
