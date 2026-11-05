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

        public void UpdatePerson(int id, string newName)
        {
            int result = 0;
            try
            {
                Init();

                // Validar que el nuevo nombre no esté vacío
                if (string.IsNullOrEmpty(newName))
                    throw new Exception("El nuevo nombre es requerido");

                // crear el objeto Persona con el Id existente y el nuevo nombre
                Persona personaActualizada = new Persona { Id = id, Name = newName };

                // El método Update de SQLite buscará el registro por el Id y reemplazará los demás campos
                result = _conn.Update(personaActualizada);

                Message = "Dato actualizado exitosamente";
            }
            catch (Exception ex)
            {
                Message = string.Format("Error al actualizar: " + ex.Message);
            }
        }
    }

}
