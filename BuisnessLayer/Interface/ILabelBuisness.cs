using CommonLayer.ReqModels;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Interface
{
    public interface ILabelBuisness
    {
       public LabelEntity AddLabel(int noteId, int userId, LabelModel model);

        public LabelEntity GetLabelsById(int noteId, int userId, int labelId);

        public LabelEntity removeLabel(int noteId, int userId, int labelId);
    }
}