﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIR
{
    class Queries
    {

        private static Queries baseInstance = null;
        public LinqToSQLDataContext dc = new LinqToSQLDataContext(Properties.Settings.Default.TransportCompanyConnectionString);
        public static Queries Instance
        {
            get
            {
                if(baseInstance==null)
                {
                    baseInstance = new Queries();
                }
                return baseInstance;
            }
        }

        public Queries() { }

        public IQueryable<Pracownicy> findEmployeByPesel(string nr_pesel)
        {
            var current = from p in dc.Pracownicies
                                where p.nr_pesel == nr_pesel
                                select p;
            return current;
        }

        public IQueryable<Pracownicy>findEmploye(string inputValue)
        {
            var searchEmployes = from p in dc.Pracownicies
                                 where p.nazwisko.Contains(inputValue) || p.imie.Contains(inputValue) || p.stanowisko.Contains(inputValue) || p.nr_pesel == inputValue
                                 select p;
            return searchEmployes;
        }

        public IQueryable<Pracownicy> findEmployeByJob(string job)
        {
            var search = from p in dc.Pracownicies
                         where p.stanowisko.ToLower() == "kierowca"
                         select p;
            return search;
        }

        public void deleteEmploye(Pracownicy employe)
        {
            dc.Pracownicies.DeleteOnSubmit(employe);
            dc.SubmitChanges();
        }
        public IQueryable<Pracownicy> getAllEmployes()
        {
            var employes = from p in dc.Pracownicies
                           select p;
            return employes;
        }

        public void addEmploye(Pracownicy employe)
        {
            dc.Pracownicies.InsertOnSubmit(employe);
        }

        public IQueryable<Ciezarowki> findTir(string inputValue)
        {
            int rocznik;

            var result2 = Int32.TryParse(inputValue, out rocznik);

            if (!result2)
                rocznik = 0;
            var searchTirs = from p in dc.Ciezarowkis
                             where p.model.Contains(inputValue) || p.producent.Contains(inputValue) || p.nr_pesel_kierowcy.Contains(inputValue) || p.nr_rejestracyjny_ciezarowki == inputValue || p.rocznik == rocznik
                             select p;
            return searchTirs;
        }


        public IQueryable<Ciezarowki>getAllTirs()
        {
            var tirs = from p in dc.Ciezarowkis
                       select p;
            return tirs;
        }

        public void deleteTir(Ciezarowki tir)
        {
            dc.Ciezarowkis.DeleteOnSubmit((Ciezarowki)tir);
            dc.SubmitChanges();
        }
        


        public IQueryable<Ciezarowki>findTirByNr(string nr)
        {
            var search = from p in dc.Ciezarowkis
            where p.nr_rejestracyjny_ciezarowki == nr
            select p;
            return search;
        }

        public void addTir(Ciezarowki tir)
        {
            dc.Ciezarowkis.InsertOnSubmit(tir);
        }

        public void submitChanges()
        {
            dc.SubmitChanges();
        }

        public Ciezarowki findTirByPesel(string pesel)
        {
            var tirExists = (from p in dc.Ciezarowkis
                             where p.nr_pesel_kierowcy == pesel
                             select p).Any();
            if (tirExists)
            {
                var search = (from p in dc.Ciezarowkis
                              where p.nr_pesel_kierowcy == pesel
                              select p).First();
                return search;
            }
            return null;
            
        }


        public IQueryable<Ladunki> getAllCargos()
        {
            var cargos = from p in dc.Ladunkis
                         select p;
            return cargos;
        }

        public IQueryable<Ladunki> findCargo(string inputValue)
        {
            int number=0;
            bool result = Int32.TryParse(inputValue, out number);

            var cargo = from p in dc.Ladunkis
                        from k in dc.Kliencis
                        where p.id_ladunku == number || p.nazwa_ladunku == inputValue || (p.id_nadawcy == k.id_klienta && (k.imie.Contains(inputValue) || k.nazwisko.Contains(inputValue)))
                        select p;
            return cargo;
        }


        public void deleteCargo(Ladunki cargo)
        {
            dc.Ladunkis.DeleteOnSubmit((Ladunki)cargo);
            dc.SubmitChanges();
        }
    }
}
