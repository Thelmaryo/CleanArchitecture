using College.Enumerators;
using College.Helpers;
using College.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace College.Controllers
{
    public class ProfessorController : ControllerBase
    {
        // GET: Professor
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var professor = new Professor();
            return View(professor.List());
        }

        // GET: Professor/Details/5
        public ActionResult Details(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var professor = new Professor();
            professor.Get(id);
            return View(professor);
        }

        // GET: Professor/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var names = Enum.GetNames(typeof(EDegree));
            var values = Enum.GetValues(typeof(EDegree));
            List<ComboboxItem> combobox = new List<ComboboxItem>();
            for (int index = 0; index < names.Count(); index++)
            {
                var value = values.GetValue(index).ToString();
                combobox.Add(new ComboboxItem(names[index], value));
            }
            ViewBag.Degrees = new SelectList(combobox, "Value", "Text");
            return View();
        }

        // POST: Professor/Create
        [HttpPost]
        public ActionResult Create(Professor professor)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            try
            {
                if (!ModelState.IsValid)
                    return View(professor);
                var names = Enum.GetNames(typeof(EDegree));
                var values = Enum.GetValues(typeof(EDegree));
                List<ComboboxItem> combobox = new List<ComboboxItem>();
                for (int index = 0; index < names.Count(); index++)
                {
                    var value = values.GetValue(index).ToString();
                    combobox.Add(new ComboboxItem(names[index], value));
                }
                ViewBag.Degrees = new SelectList(combobox, "Value", "Text");
                // TODO: Add insert logic here
                if (professor.FirstName.Length < 3)
                {
                    ModelState.AddModelError("FirstName", "O Nome deve ter no minimo 3 caracteres");
                    return View(professor);
                }
                if (professor.LastName.Length < 3)
                {
                    ModelState.AddModelError("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
                    return View(professor);
                }
                if (professor.Phone.Length < 8)
                {
                    ModelState.AddModelError("Telefone", "O Telefone deve ter no minimo 8 caracteres");
                    return View(professor);
                }


                Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

                if (!rg.IsMatch(professor.Email))
                {
                    ModelState.AddModelError("Email", "Email Inválido!");
                    return View(professor);
                }
                if (IsCpf(professor.CPF))
                {
                    ModelState.AddModelError("CPF", "CPF Inválido!");
                    return View(professor);
                }
                professor.Password = professor.CPF.Replace("-", "").Replace(".", "");

                // Cria um salt aleatório de 64 bits
                byte[] salt = new byte[8];
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    // Enche o array com um valor aleatório
                    rngCsp.GetBytes(salt);
                }
                // Escolha o valor mais alto que seja "tolerável"
                // 100 000 era um valor razoável em 2011, não sei se é suficiente hoje
                int myIterations = 100000;
                Rfc2898DeriveBytes k = new Rfc2898DeriveBytes(professor.Password, salt, myIterations);
                professor.Salt = String.Join(",", salt);

                professor.Password = Convert.ToBase64String(k.GetBytes(32));
                // Codifica esse Password de alguma forma e salva no BD
                // (lembre-se de salvar o salt também! você precisará dele para comparação)

                professor.Create();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(professor);
            }
        }

        // GET: Professor/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var professor = new Professor();
            professor.Get(id);
            var names = Enum.GetNames(typeof(EDegree));
            var values = Enum.GetValues(typeof(EDegree));
            List<ComboboxItem> combobox = new List<ComboboxItem>();
            for (int index = 0; index < names.Count(); index++)
            {
                var value = values.GetValue(index).ToString();
                combobox.Add(new ComboboxItem(names[index], value));
            }
            ViewBag.Degrees = new SelectList(combobox, "Value", "Text");
            return View(professor);
        }

        // POST: Professor/Edit/5
        [HttpPost]
        public ActionResult Edit(Professor professor)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            try
            {
                if (!ModelState.IsValid)
                    return View(professor);
                var names = Enum.GetNames(typeof(EDegree));
                var values = Enum.GetValues(typeof(EDegree));
                List<ComboboxItem> combobox = new List<ComboboxItem>();
                for (int index = 0; index < names.Count(); index++)
                {
                    var value = values.GetValue(index).ToString();
                    combobox.Add(new ComboboxItem(names[index], value));
                }
                ViewBag.Degrees = new SelectList(combobox, "Value", "Text");
                // TODO: Add insert logic here
                if (professor.FirstName.Length <= 3)
                {
                    ModelState.AddModelError("FirstName", "O Nome deve ter no minimo 3 caracteres");
                    return View(professor);
                }
                if (professor.LastName.Length <= 3)
                {
                    ModelState.AddModelError("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
                    return View(professor);
                }
                if (professor.Phone.Length <= 8)
                {
                    ModelState.AddModelError("Telefone", "O Telefone deve ter no minimo 8 caracteres");
                    return View(professor);
                }


                Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

                if (!rg.IsMatch(professor.Email))
                {
                    ModelState.AddModelError("Email", "Email Inválido!");
                    return View(professor);
                }
                if (!IsCpf(professor.CPF))
                {
                    ModelState.AddModelError("CPF", "CPF Inválido!");
                    return View(professor);
                }
                // TODO: Add update logic here
                professor.Edit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(professor);
            }
        }

        // GET: Professor/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var professor = new Professor();
            professor.Get(id);
            return View(professor);
        }

        // POST: Professor/Delete/5
        [HttpPost]
        public ActionResult Delete(Professor professor)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            try
            {
                // TODO: Add delete logic here
                professor.Delete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(professor);
            }
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
