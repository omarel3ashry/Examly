﻿using DataAccessLibrary.Model;

namespace WebAppProject.VMS
{
    public class StudentExamsViewModel
    {
        public List<ExamViewModel> CommingExams;
        public List<ExamTakenViewModel> ExamsTaken;
        public List<ExamViewModel> MissedExams;
    }
}