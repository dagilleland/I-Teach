using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
namespace ITeach.CourseEvaluation.DomainModel
{
    internal class Course
    {
        public string Number { get; private set; }
        public Evaluation DraftEvaluation { get; private set; }
        public Evaluation ApprovedEvaluation { get; private set; }
        public Course(string number)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(number), "course number is null or empty.");
            Number = number;
        }
        public void ApproveDraftEvalation()
        {
            Contract.Assume(DraftEvaluation != null, "draft evaluation has not been created.");
            ApprovedEvaluation = DraftEvaluation;
            DraftEvaluation = null;
        }
        public void CreateDraftEvaluation(Evaluation evaluation)
        {
            Contract.Requires(evaluation != null, "evaluation is null.");
            Contract.Assume(DraftEvaluation == null, "draft evaluation already exists");
            DraftEvaluation = evaluation;
        }
        public void ProposeEvaluationGroup(EvaluationGroup group)
        {
            Contract.Requires(group != null, "evaluation group is null.");
            Contract.Assume(DraftEvaluation != null, "draft evaluation has not been created.");
            DraftEvaluation.EvaluationGroups.Add(group);
        }
        public void AddEvaluationToGroup(string groupName, EvaluationItem item)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(groupName), "groupName is null or empty.");
            Contract.Requires(item != null, "item is null.");
            Contract.Assume(DraftEvaluation != null, "draft evaluation has not been created.");
            DraftEvaluation.EvaluationGroups.Single(x=>x.Name==groupName).Items.Add(item);
        }
        public void AddEvaluation(EvaluationItem item)
        {
            Contract.Requires(item != null, "item is null.");
            Contract.Assume(DraftEvaluation != null, "draft evaluation has not been created.");
            DraftEvaluation.IndependentItems.Add(item);
        }
    }
    internal class Evaluation
    {
        public IList<EvaluationGroup> EvaluationGroups { get; set; }
        public IList<EvaluationItem> IndependentItems { get; set; }

        public Evaluation()
        {
            EvaluationGroups = new List<EvaluationGroup>();
            IndependentItems = new List<EvaluationItem>();
        }
    }
    internal class EvaluationGroup
    {
        public string Name { get; set; }
        public int? PassMark { get; set; }
        public IList<EvaluationItem> Items { get; set; }

        public EvaluationGroup(string name, int? passMark)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(name), "name is null or empty.");
            Contract.Requires(!passMark.HasValue || (passMark.Value > 0 || passMark.Value < 100), "pass mark is not between 0 and 100");

            Name = name;
            PassMark = passMark;
            Items = new List<EvaluationItem>();
        }
    }
    internal class EvaluationItem
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        public EvaluationItem(string name, int weight)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(name), "name is null or empty.");
            Contract.Requires(weight > 0, "weighting is zero or less");
            Contract.Requires(weight <= 100, "weight is greater than 100 %");
            Name = name;
            Weight = weight;
        }
    }
}
