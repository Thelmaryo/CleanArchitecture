using College.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace College.Controllers
{
    public class StudentController : ControllerBase
    {
        // GET: Student
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var student = new Student();
            return View(student.List());
        }

        // GET: Student/Details/5
        public ActionResult Details(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var student = new Student();
            student.Get(id);
            ViewBag.Course = new Course().List().SingleOrDefault(x => x.Id == student.CourseId).Name;
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            ViewBag.ListarCurso = new SelectList(new Course().List().OrderBy(x => x.Name), "Id", "Name");
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                student.Password = student.CPF.Replace("-", "").Replace(".", "");
                if (student.FirstName.Length < 3 || student.FirstName == null)
                {
                    ModelState.AddModelError("FirstName", "O Nome deve ter no minimo 3 caracteres");
                    return View(student);
                }
                if (student.LastName.Length < 3 || student.LastName == null)
                {
                    ModelState.AddModelError("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
                    return View(student);
                }
                if (student.Phone.Length < 8 || student.Phone == null)
                {
                    ModelState.AddModelError("Telefone", "O Telefone deve ter no minimo 8 caracteres");
                    return View(student);
                }

                Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

                if (!rg.IsMatch(student.Email))
                {
                    ModelState.AddModelError("Email", "Email Inválido!");
                    return View(student);
                }

                if (!IsCpf(student.CPF))
                {
                    ModelState.AddModelError("CPF", "CPF Inválido!");
                    return View(student);
                }

                Student student1 = new Student();
                student1.Get(student.CPF);
                if (student1.CPF == student.CPF)
                {
                    ModelState.AddModelError("CPF", "CPF já foi cadastrado!");
                    return View(student);
                }

                if (student.Address == string.Empty || student.Address == null)
                {
                    ModelState.AddModelError("Address", "O endereço é campo obrigatorio!");
                    return View(student);
                }
                if (student.City == string.Empty || student.City == null)
                {
                    ModelState.AddModelError("City", "A cidade é campo obrigatorio!");
                    return View(student);
                }
                if (student.Country == string.Empty || student.Country == null)
                {
                    ModelState.AddModelError("Country", "O país é campo obrigatorio!");
                    return View(student);
                }
                if (student.Email == string.Empty || student.Email == null)
                {
                    ModelState.AddModelError("Email", "O Email é campo obrigatorio!");
                    return View(student);
                }

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
                Rfc2898DeriveBytes k = new Rfc2898DeriveBytes(student.Password, salt, myIterations);
                student.Salt = String.Join(",", salt);

                student.Password = Convert.ToBase64String(k.GetBytes(32));
                // Codifica esse Password de alguma forma e salva no BD
                // (lembre-se de salvar o salt também! você precisará dele para comparação)

                // TODO: Add insert logic here
                student.Create();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(student);
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var student = new Student();
            student.Get(id);
            ViewBag.ListarCurso = new SelectList(new Course().List().OrderBy(x => x.Name), "Id", "Name");
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            try
            {
                if (student.FirstName.Length < 3 || student.FirstName == null)
                {
                    ModelState.AddModelError("FirstName", "O Nome deve ter no minimo 3 caracteres");
                    return View(student);
                }
                if (student.LastName.Length < 3 || student.LastName == null)
                {
                    ModelState.AddModelError("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
                    return View(student);
                }
                if (student.Phone.Length < 8 || student.Phone == null)
                {
                    ModelState.AddModelError("Telefone", "O Telefone deve ter no minimo 8 caracteres");
                    return View(student);
                }

                Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

                if (!rg.IsMatch(student.Email))
                {
                    ModelState.AddModelError("Email", "Email Inválido!");
                    return View(student);
                }

                if (!IsCpf(student.CPF))
                {
                    ModelState.AddModelError("CPF", "CPF Inválido!");
                    return View(student);
                }

                Student student1 = new Student();
                student1.Get(student.CPF);
                if (student1.CPF == student.CPF)
                {
                    ModelState.AddModelError("CPF", "CPF já foi cadastrado!");
                    return View(student);
                }

                if (student.Address == string.Empty || student.Address == null)
                {
                    ModelState.AddModelError("Address", "O endereço é campo obrigatorio!");
                    return View(student);
                }
                if (student.City == string.Empty || student.City == null)
                {
                    ModelState.AddModelError("City", "A cidade é campo obrigatorio!");
                    return View(student);
                }
                if (student.Country == string.Empty || student.Country == null)
                {
                    ModelState.AddModelError("Country", "O país é campo obrigatorio!");
                    return View(student);
                }
                if (student.Email == string.Empty || student.Email == null)
                {
                    ModelState.AddModelError("Email", "O Email é campo obrigatorio!");
                    return View(student);
                }
                // TODO: Add update logic here
                student.Edit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(student);
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var student = new Student();
            student.Get(id);
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(Student student)
        {
            try
            {
                // TODO: Add delete logic here
                student.Delete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(student);
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