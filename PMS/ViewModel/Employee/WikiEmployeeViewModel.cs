using PMS.DAL;
using PMS.Model;
using PMS.ViewModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PMS
{
    public class WikiEmployeeViewModel : BindableBase
    {

        private readonly PMSContext dbContext = new PMSContext();

        //Lista
        private ObservableCollection<Article> _Articles { get; set; }

        //Dodanie
        private string _Url = "";
        private string _Description = "";
        public ICommand AddArticleButton { get; set; }

        public WikiEmployeeViewModel()
        {
            AddArticleButton = new DelegateCommand(AddArticle);
        }

        public ObservableCollection<Article> Articles
        {
            get
            {
                if (_Articles == null)
                {
                    _Articles = new ObservableCollection<Article>(dbContext.Article);
                }
                return _Articles;
            }
            set
            {
                _Articles = value;
                RaisePropertyChanged("Articles");
            }
        }

        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
                RaisePropertyChanged("Url");
            }
        }
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                RaisePropertyChanged("Description");
            }
        }

        private void AddArticle()
        {

            EmployeeValidation VM = new EmployeeValidation();
            bool correctForm = VM.WikiValidation(Url, Description);

            if (correctForm)
            {
                Article article = new Article()
                {
                    Url = _Url,
                    Description = _Description,
                    DateAdded = DateTime.Now,
                    UserID = Global.user.UserID
                };

                dbContext.Article.Add(article);
                dbContext.SaveChanges();

                Articles = new ObservableCollection<Article>(dbContext.Article);

                ErrorMessage er = new ErrorMessage("Add article!");
                er.ShowDialog();
            }
        }
    }
}
