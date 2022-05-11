﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
    using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class MedicationApprovalRequestController
    {
        private readonly MedicationApprovalRequestService _medicationApprovalRequestService;

        public MedicationApprovalRequestController(MedicationApprovalRequestService medicationApprovalRequestService)
        {
            _medicationApprovalRequestService = medicationApprovalRequestService;
        }

        public IEnumerable<MedicationApprovalRequest> GetAll()
        {
            return _medicationApprovalRequestService.GetAll();
        }
        public IEnumerable<MedicationApprovalRequest> GetAllByApprovingDoctorId(long doctorId)
        {
            return _medicationApprovalRequestService.GetAllByApprovingDoctorId(doctorId);
        }
        public MedicationApprovalRequest GetById(long id)
        {
            return _medicationApprovalRequestService.GetById(id);
        }
        public MedicationApprovalRequest Create(MedicationApprovalRequest medicationApprovalRequest)
        {
            return _medicationApprovalRequestService.Create(medicationApprovalRequest);
        }
        public bool Update(MedicationApprovalRequest medicationApprovalRequest)
        {
            return _medicationApprovalRequestService.Update(medicationApprovalRequest);
        }
        public bool Delete(long medicationApprovalRequestId)
        {
            return _medicationApprovalRequestService.Delete(medicationApprovalRequestId);
        }
    }
}
