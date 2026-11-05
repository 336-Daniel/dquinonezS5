using dquinonezS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace dquinonezS5.Repositories
{
    public class PersonaRepository
    {
        string _dbPath; //ruta
        private SQLiteConnection _conn;
        public string Message { get; set; }

        public void Init()
        {
            if (_conn is not null)
                return;
            _conn = new(_dbPath);
            _conn.CreateTable<Persona>();
        }

        public PersonaRepository (string Path)
        {
            _dbPath = Path;
        }

        public void AddNewPerson(string name)
        {
           int result = 0;

            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");
                Persona persona = new() { Name = name };
                result = _conn.Insert(persona);
                Message = string.Format("Dato ingresado");


            }
            catch (Exception ex)
            {
                Message = string.Format("Error " + ex.Message);
               
            }
        }

        public List<Persona> GetAllPersonas()
        {
            try
            {
                Init();
                return _conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                Message = string.Format("Error " + ex.Message);
            }
            return new List<Persona>();
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                _conn.Delete<Persona>(id);
                Message = "Dato eliminado exitosamente";
            }
            catch (Exception ex)
            {
                Message = string.Format("Error " + ex.Message);
            }
        }
    }

}
