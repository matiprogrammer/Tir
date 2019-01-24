using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIR
{
    class Queries
    {

        // private static Queries baseInstance = null;
        private LinqToSQLDataContext dc;
        //public static Queries Instance
        //{
        //    get
        //    {
        //        if(baseInstance==null)
        //        {
        //            baseInstance = new Queries();
        //        }
        //        return baseInstance;
        //    }
        //}

        public Queries() { dc = new LinqToSQLDataContext(Properties.Settings.Default.TransportCompanyConnectionString); }

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
            var employes = (from p in dc.Pracownicies
                            where p.nr_pesel==employe.nr_pesel
                            select p).First();

            dc.Pracownicies.DeleteOnSubmit(employes);
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
            dc.SubmitChanges();
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
            var tirs = (from c in dc.Ciezarowkis
                            where c.nr_rejestracyjny_ciezarowki == tir.nr_rejestracyjny_ciezarowki
                            select c).First();
            dc.Ciezarowkis.DeleteOnSubmit(tirs);
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
            dc.SubmitChanges();
        }

        public void submitChanges()
        {
            dc.SubmitChanges();
        }

        public Ciezarowki findTirByPesel(string pesel)
        {
                var search = (from p in dc.Ciezarowkis
                              where p.nr_pesel_kierowcy == pesel
                              select p).First();
                return search;
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

        public IQueryable<Ladunki> findCargoById(int id)
        {
            var cargo = (from p in dc.Ladunkis
                          where p.id_ladunku == id
                          select p);
            Console.WriteLine(id);
            return cargo;
        }
        public void addCargo(Ladunki newCargo)
        {
            dc.Ladunkis.InsertOnSubmit(newCargo);
            dc.SubmitChanges();
        }

        public void deleteCargo(Ladunki cargo)
        {
            var cargos = (from c in dc.Ladunkis
                          where c.id_ladunku == cargo.id_ladunku
                          select c).First();
            dc.Ladunkis.DeleteOnSubmit(cargos);
            dc.SubmitChanges();
        }

        #region Klienci
        public IQueryable<Klienci> getAllCustomers()
        {
            var customers = from p in dc.Kliencis
                            select p;
            return customers;
        }

        public IQueryable<Klienci> findCustomer(string inputValue)
        {
            int inputNumber = 0;
            bool result = Int32.TryParse(inputValue, out inputNumber);

            var searchCustomers = from p in dc.Kliencis
                                 where p.nazwisko.Contains(inputValue) || p.imie.Contains(inputValue) || p.adres_zamieszkania.Contains(inputValue)
                                  select p;
            return searchCustomers;
        }

        public IQueryable<Klienci> findCustomerByID(int givenID)
        {
            var searchCustomer = (from p in dc.Kliencis
                                  where p.id_klienta == givenID
                                  select p);
            return searchCustomer;
        }

        public void addCustomer(Klienci newCustomer)
        {
            dc.Kliencis.InsertOnSubmit(newCustomer);
            //dc.SubmitChanges();
        }

        public void deleteCustomer(Klienci customerToDelete)
        {
            var customers = (from k in dc.Kliencis
                             where k.id_klienta == customerToDelete.id_klienta
                             select k).First();
            dc.Kliencis.DeleteOnSubmit(customers);
            dc.SubmitChanges();
        }
        #endregion

        #region Firmy serwisujące
        public IQueryable<Firmy_serwisujace> getAllCompanies()
        {
            var companies = from p in dc.Firmy_serwisujaces
                            select p;
            return companies;
        }

        public IQueryable<Firmy_serwisujace> findCompany(string inputValue)
        {
            int inputNumber = 0;
            bool result = Int32.TryParse(inputValue, out inputNumber);

            var searchCompanies = from p in dc.Firmy_serwisujaces
                                  where p.nazwa.Contains(inputValue) || p.adres.Contains(inputValue) || p.nr_telefonu.Contains(inputValue) || p.nr_nip == inputNumber
                                  select p;
            return searchCompanies;
        }

        public IQueryable<Firmy_serwisujace> findCompanyByNIP(int givenNIP)
        {
            var searchCompanies = (from p in dc.Firmy_serwisujaces
                                  where p.nr_nip == givenNIP
                                  select p);
            return searchCompanies;
        }

        public void addCompany(Firmy_serwisujace newCompany)
        {
            dc.Firmy_serwisujaces.InsertOnSubmit(newCompany);
            dc.SubmitChanges();
        }

        public void deleteCompany(Firmy_serwisujace companyToDelete)
        {
            var companies = (from k in dc.Firmy_serwisujaces
                             where k.nr_nip == companyToDelete.nr_nip
                             select k).First();
            dc.Firmy_serwisujaces.DeleteOnSubmit(companies);
            dc.SubmitChanges();
        }
        #endregion

        #region Ładunki




        #endregion
    }
}
