using CommonLayer.ReqModels;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRepo : ILabelRepo
    {
        private readonly FundooDBContext Context;


        public LabelRepo(FundooDBContext Context)
        {
            this.Context = Context;

        }

        public LabelEntity AddLabel(int noteId, int userId, LabelModel model)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity.NoteId = noteId;
                labelEntity.userID = userId;
                labelEntity.labelName = model.label;
                labelEntity.CreatedAt = DateTime.Now;
                labelEntity.UpdatedAt = DateTime.Now;
                Context.Add(labelEntity);
                Context.SaveChanges();
                return labelEntity;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<LabelEntity> GetLabels()
        {
            try
            {
                List<LabelEntity> labelList = Context.Labels.ToList();
                return labelList;
            }catch (Exception ex) { throw ex; }
        }

        public LabelEntity GetLabelsById(int noteId,int userId,int labelId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity = Context.Labels.Where(x=>x.NoteId==noteId && x.userID==userId && x.labelId==labelId).FirstOrDefault();
                if(labelEntity != null) { return labelEntity; }
                return null;

            }
            catch (Exception ex) { throw ex; }
        }

        public LabelEntity removeLabel(int noteId, int userId, int labelId)
        {
            try
            {
               LabelEntity label = Context.Labels.Where(x => x.NoteId == noteId && x.userID == userId && x.labelId == labelId).FirstOrDefault();
               if(label != null) { Context.Labels.Remove(label); Context.SaveChanges(); return label; }
                return null;
            }
            catch(Exception ex) { throw ex; }   
        }
    }
}
