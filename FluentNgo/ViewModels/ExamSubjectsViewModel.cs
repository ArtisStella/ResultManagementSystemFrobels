using TFSResult.Core;
using TFSResult.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace TFSResult.ViewModels
{
    public class ExamSubjectsViewModel : ObservableObject
    {
        private ICollectionView _examSubjectsCollection;

        public ICollectionView ExamSubjectsCollection
        {
            get { return _examSubjectsCollection; }
            set { _examSubjectsCollection = value; }
        }

        private ObservableCollection<ExamSubjects> _examSubjectsList;
        public ObservableCollection<ExamSubjects> ExamSubjectsList
        {
            get { return _examSubjectsList; }
            set
            {
                if (value == _examSubjectsList)
                    return;
                _examSubjectsList = value;
                OnPropertyChanged("ExamSubjectsList");
            }
        }

        public List<Exam> ExamList { get; set; }
        public List<Subject> SubjectList { get; set; }

        public ExamSubjectsViewModel()
        {
            ExamSubjectsList = new ObservableCollection<ExamSubjects>();
            ExamList = Exam.ExamGetAll();
            SubjectList = Subject.SubjectGetAll();
            ExamSubjectsCollection = CollectionViewSource.GetDefaultView(ExamSubjectsList);
        }

        public void ExamSubjectsGetAll(int examId)
        {
            ExamSubjectsList.Clear();
            foreach(ExamSubjects examSubject in ExamSubjects.ExamSubjectGetAllByExamId(examId))
            {
                ExamSubjectsList.Add(examSubject);
            }
        }

        public void AddExamSubject(int ExamId, Subject subject)
        {
            if (ExamSubjectsList.Any(es => es.SubjectId == subject.SubjectId)) return;

            ExamSubjects examSubject = new ExamSubjects() { ExamId = ExamId, SubjectId = subject.SubjectId, SubjectName = subject.SubjectName };
            ExamSubjectsList.Add(examSubject);
        }

        public void RemoveExamSubject(ExamSubjects examSubject)
        {
            ExamSubjectsList.Remove(examSubject);
        }

        public void EditExamSubject(ExamSubjects examSubject)
        {

        }

        public bool SaveExamSubjects()
        {
            return ExamSubjects.UpdateExamSubjects(ExamSubjectsList.ToList());
        }
    }
}
