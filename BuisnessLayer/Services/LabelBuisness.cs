using BuisnessLayer.Interface;
using CommonLayer.ReqModels;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class LabelBuisness : ILabelBuisness
    {
        private ILabelRepo ilabel;

        public LabelBuisness(ILabelRepo labelRepo)
        {
            this.ilabel = labelRepo;
        }

        public LabelEntity AddLabel(int noteId, int userId, LabelModel model)
        {
            return ilabel.AddLabel(noteId, userId, model);
        }

        public LabelEntity GetLabelsById(int noteId, int userId, int labelId)
        {
            return ilabel.GetLabelsById(noteId, userId, labelId);
        }

        public LabelEntity removeLabel(int noteId, int userId, int labelId)
        {
            return ilabel.removeLabel(noteId, userId, labelId);
        }
    }
}
