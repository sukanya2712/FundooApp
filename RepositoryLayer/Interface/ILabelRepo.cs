using CommonLayer.ReqModels;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepo
    {
        LabelEntity AddLabel(int noteId, int userId, LabelModel model);

        public LabelEntity GetLabelsById(int noteId, int userId, int labelId);

        public LabelEntity removeLabel(int noteId, int userId, int labelId);

    }
}