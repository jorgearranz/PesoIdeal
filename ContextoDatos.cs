using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;

namespace PesoIdeal
{
	public class ContextoDatos:DataContext
	{
		public ContextoDatos():base(DBConnectionString)
		{
		
		
		}
		
		public Table<User> Users;
		public Table<DataUser>Datas;

		public static string DBConnectionString = "Data Source=isostore:/pesos.sdf";

		public static bool DatabaseExist()
		{
			 using (ContextoDatos db = new ContextoDatos())
             {
				 return db.DatabaseExists();
			 }
		}
		public static void CrearDBSiNoExiste()
        {
            using (ContextoDatos db = new ContextoDatos())
            {
                if (!db.DatabaseExists())
                {
                    db.CreateDatabase();
                }
            }
        }
		public static void DeleteDB()
        {
            using (ContextoDatos db = new ContextoDatos())
            {
                if (db.DatabaseExists())
                {
                    db.DeleteDatabase();
                }
            }
        }
	}

	

	

	[Table(Name="User")]
    public class User : INotifyPropertyChanged, INotifyPropertyChanging
    {
         private int _Id;
 
        [Column(DbType="INT NOT NULL IDENTITY", IsPrimaryKey = true, 
                IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _Id; }
            set
            {
                NotifyPropertyChanging("Id");
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }
 
        private string _Nombre;
 
        [Column(DbType="NVARCHAR(50) NOT NULL", CanBeNull=false)]
        public string Nombre 
        {
            get { return _Nombre; }
            set
            {
                NotifyPropertyChanging("Nombre");
                _Nombre = value;
                NotifyPropertyChanged("Nombre");
            }
        }
 
       
 
        #region INotifyPropertyChanged Members
 
        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
 
        #endregion
 
        #region INotifyPropertyChanging Members
 
        public event PropertyChangingEventHandler PropertyChanging;
 
        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
 
        #endregion
    }

	
    [Table(Name = "DataUser")]
    public class DataUser : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _Id;
 
        [Column(DbType="INT NOT NULL IDENTITY", IsPrimaryKey = true, 
                IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _Id; }
            set
            {
                NotifyPropertyChanging("Id");
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }
 
        private string _Genero;
        [Column(DbType = "NVARCHAR(25) NOT NULL", CanBeNull = false)]
        public string Genero
        {
            get { return _Genero; }
            set
            {
                NotifyPropertyChanging("Genero");
                _Genero = value;
                NotifyPropertyChanged("Genero");
            }
        }

		 private double _Altura;
        [Column(DbType = "FLOAT NOT NULL", CanBeNull = false)]
        public double Altura
        {
            get { return _Altura; }
            set
            {
                NotifyPropertyChanging("Altura");
                _Altura = value;
                NotifyPropertyChanged("Altura");
            }
        }
 
		private int _Edad;
        [Column(DbType = "INT NOT NULL", CanBeNull = false)]
        public int Edad
        {
            get { return _Edad; }
            set
            {
                NotifyPropertyChanging("Edad");
                _Edad = value;
                NotifyPropertyChanged("Edad");
            }
        }
 
		private double _Peso;
 
        [Column(DbType = "FLOAT NOT NULL", CanBeNull = false)]
        public double Peso
        {
            get { return _Peso; }
            set
            {
                NotifyPropertyChanging("Peso");
                _Peso = value;
                NotifyPropertyChanged("Peso");
            }
        }
 
		private double _Indice;
        [Column(DbType = "FLOAT NOT NULL", CanBeNull = false)]
        public double Indice
        {
            get { return _Indice; }
            set
            {
                NotifyPropertyChanging("Indice");
                _Indice = value;
                NotifyPropertyChanged("Indice");
            }
        }
 

        private DateTime _Fecha;
 
        [Column(DbType = "DATETIME NULL", CanBeNull = true)]
        public DateTime Fecha
        {
            get { return _Fecha; }
            set
            {
                NotifyPropertyChanging("Fecha");
                _Fecha= value;
                NotifyPropertyChanged("Fecha");
            }
        }
 
		
		private int _IdUsuario;
 
        [Column(Name = "IdUsuario", DbType = "INT NOT NULL", CanBeNull = false)]
        public int IdUsuario
        {
            get { return this._IdUsuario; }
            set
            {
                NotifyPropertyChanging("IdUsuario");
                _IdUsuario = value;
                NotifyPropertyChanged("IdUsuario");
            }
        }
        private EntityRef<User> _User;
 
        [Association(Name="FK_DataUser_User", IsForeignKey=true,
            ThisKey="IdUsuario", OtherKey="Id")]
        public User User
        {
            get { return this._User.Entity; }
            set
            {
                NotifyPropertyChanging("User");
                _User.Entity = value;
                NotifyPropertyChanged("User");
            }
        }
 
		//private char _CodigoPosicion;
 
		//[Column(Name="Posicion", DbType = "NCHAR NOT NULL", CanBeNull = false)]
		//public char CodigoPosicion
		//{
		//    get { return _CodigoPosicion; }
		//    set
		//    {
		//        NotifyPropertyChanging("Posicion");
		//        char ch = char.ToUpperInvariant(value);
		//        if ("PDML".IndexOf(ch) == -1)
		//            throw new InvalidOperationException("Posición incorrecta: " + ch);
		//        _CodigoPosicion = ch;
		//        NotifyPropertyChanged("Posicion");
		//    }
		//}

        #region INotifyPropertyChanged Members
 
        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
 
        #endregion
 
        #region INotifyPropertyChanging Members
 
        public event PropertyChangingEventHandler PropertyChanging;
 
        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
 
        #endregion
        // Se omite la implementación de las interfaces de notificación de cambios
    }





	//private void buttonEnviar_Click(object sender, RoutedEventArgs e)
	//    {
	//        switch (accionCUD)
	//        {
	//            case AccionCUD.Insertar:
	//                try
	//                {
	//                    equipo = new Equipo()
	//                    {
	//                        Id = textBoxId.Text.ToUpperInvariant(),
	//                        Nombre = textBoxNombre.Text,
	//                        Ciudad = textBoxCiudad.Text,
	//                    };
	//                    using (Futbol2006DataContext ctx = new Futbol2006DataContext())
	//                    {
	//                        ctx.GetTable<Equipo>().InsertOnSubmit(equipo);
	//                        ctx.SubmitChanges();
	//                    }
	//                    TeamsMenu();
	//                }
	//                catch (DbException x)
	//                {
	//                    MessageBox.Show("Error al insertar: " + x.Message);
	//                }
	//                break;
	//            case AccionCUD.Actualizar:
	//                try
	//                {
	//                    using (Futbol2006DataContext ctx = new Futbol2006DataContext())
	//                    {
	//                        ctx.GetTable<Equipo>().Attach(equipo);
	//                        equipo.Nombre = textBoxNombre.Text;
	//                        equipo.Ciudad = textBoxCiudad.Text;
	//                        ctx.SubmitChanges();
	//                    }
	//                    TeamsMenu();
	//                }
	//                catch (DbException x)
	//                {
	//                    MessageBox.Show("Error al actualizar: " + x.Message);
	//                }
	//                break;
	//            case AccionCUD.Eliminar:
	//                try
	//                {
	//                    using (Futbol2006DataContext ctx = new Futbol2006DataContext())
	//                    {
	//                        ctx.GetTable<Equipo>().Attach(equipo);
	//                        ctx.GetTable<Equipo>().DeleteOnSubmit(equipo);
	//                        ctx.SubmitChanges();
	//                    }
	//                    TeamsMenu();
	//                }
	//                catch (DbException x)
	//                {
	//                    MessageBox.Show("Error al eliminar: " + x.Message);
	//                }
	//                break;
	//        }
	//    }
 
		//private void buttonBuscar_Click(object sender, RoutedEventArgs e)
		//{
		//    using (Futbol2006DataContext ctx = new Futbol2006DataContext())
		//    {
		//        equipo = (from eq in ctx.GetTable<Equipo>()
		//                  where eq.Id == textBoxId.Text
		//                  select eq).FirstOrDefault();
		//        if (equipo != null)
		//        {
		//            DesactivarBusqueda();
		//            textBoxNombre.Text = equipo.Nombre;
		//            textBoxCiudad.Text = equipo.Ciudad;
		//            PrepararIU();
		//        }
		//        else
		//            MessageBox.Show("Equipo no encontrado");
		//    }
		//}


}
