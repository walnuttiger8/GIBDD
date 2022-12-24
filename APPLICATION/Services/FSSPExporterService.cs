using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Services
{
    public class FSSPExporterService
    {
        private readonly GIBDDEntities _db;
        private const int _SENT_TO_FSSP_STATUS_ID = 1;

        public FSSPExporterService()
        {
            _db = new GIBDDEntities();
        }

        public List<Fines> GetFinesToExport()
        {
            var statusesToSend = _db.FineStatuses.Where(x => x.SendToFSSP).Select(x => x.Id).ToList();
            var result = new List<Fines>();

            foreach (var fine in _db.Fines)
            {
                var statusHistory = fine.FineStatusHistory;
                var lastStatusDt = statusHistory.Select(x => x.DateCreated).Max();
                var lastStatus = statusHistory.Where(x => x.DateCreated == lastStatusDt).FirstOrDefault();

                if (lastStatus != null && lastStatus.FineStatuses.SendToFSSP)
                {
                    result.Add(fine);
                }
            }

            return result;
        }

        public void SendToFSSP(List<Fines> fines)
        {
            var targetStatus = _db.FineStatuses.Where(x => x.Id == _SENT_TO_FSSP_STATUS_ID).FirstOrDefault();
            if (targetStatus == null)
            {
                throw new Exception("целевой статус не найден");
            }
            foreach (var fine in fines)
            {
                var fh = new FineStatusHistory()
                {
                    FineId = fine.Id,
                    FineStatusId = targetStatus.Id,
                    DateCreated = DateTime.Now,
                };
                _db.FineStatusHistory.Add(fh);
            }
            _db.SaveChanges();
        }
    }
}
