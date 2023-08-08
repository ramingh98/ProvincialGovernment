using Common;
using Common.DateUtil;
using Common.Objects;
using DataModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PanelBusinessLogicLayer.BusinessComponents.IdentitiesComponents;
using PanelViewModel.Filters;
using PanelViewModel.IdentitiesViewModels;

namespace PanelBusinessLogicLayer.BusinessServices.IdentitiesServices
{
    public class PersonnelService : IDisposable
    {
        private readonly PersonnelComponent _personnelComponent;

        public PersonnelService()
        {
            _personnelComponent = new PersonnelComponent();
        }

        public async Task<OperationResult> AddAsync(PersonnelViewModel personnelView)
        {
            try
            {
                var model = new PersonnelsModel()
                {
                    BirthCertificateNumber = personnelView.BirthCertificateNumber,
                    BirthDate = personnelView.BirthDate,
                    NationalCode = personnelView.NationalCode,
                    CaseNumber = personnelView.CaseNumber,
                    CaseStatusId = 1,
                    ComputerCode = personnelView.ComputerCode,
                    EducationDegreeId = personnelView.EducationDegreeId,
                    Family = personnelView.Family,
                    FatherName = personnelView.FatherName,
                    Name = personnelView.Name,
                    LastPositionId = personnelView.LastPositionId,
                    MaritalStatusId = personnelView.MaritalStatusId,
                    PlaceOfBirth = personnelView.PlaceOfBirth,
                    ServiceLocationId = personnelView.ServiceLocationId,
                    StudyFieldId = personnelView.StudyFieldId,
                    TypeOfEmploymentId = personnelView.TypeOfEmploymentId
                };
                await _personnelComponent.AddAsync(model);
                return OperationResult.Success();
            }
            catch (Exception e)
            {
                return OperationResult.Error(e.Message);
            }
        }

        public async Task<OperationResult> AddExcelAsync(List<PersonnelViewModel> personnelView)
        {
            try
            {
                var model = personnelView.Select(q => new PersonnelsModel
                {
                    BirthCertificateNumber = q.BirthCertificateNumber,
                    BirthDate = q.BirthDate,
                    NationalCode = q.NationalCode,
                    CaseNumber = q.CaseNumber,
                    CaseStatusId = q.CaseStatusId,
                    ComputerCode = q.ComputerCode,
                    EducationDegreeId = q.EducationDegreeId,
                    Family = q.Family,
                    FatherName = q.FatherName,
                    Name = q.Name,
                    LastPositionId = q.LastPositionId,
                    MaritalStatusId = q.MaritalStatusId,
                    PlaceOfBirth = q.PlaceOfBirth,
                    ServiceLocationId = q.ServiceLocationId,
                    StudyFieldId = q.StudyFieldId,
                    TypeOfEmploymentId = q.TypeOfEmploymentId
                }).ToList();
                await _personnelComponent.AddExcelAsync(model);
                return OperationResult.Success();
            }
            catch (Exception e)
            {
                return OperationResult.Error(e.Message);
            }
        }

        public async Task<OperationResult> UpdateAsync(PersonnelViewModel personnelView)
        {
            try
            {
                var model = new PersonnelsModel()
                {
                    BirthCertificateNumber = personnelView.BirthCertificateNumber,
                    BirthDate = personnelView.BirthDate,
                    NationalCode = personnelView.NationalCode,
                    CaseNumber = personnelView.CaseNumber,
                    CaseStatusId = personnelView.CaseStatusId,
                    ComputerCode = personnelView.ComputerCode,
                    EducationDegreeId = personnelView.EducationDegreeId,
                    Family = personnelView.Family,
                    FatherName = personnelView.FatherName,
                    Name = personnelView.Name,
                    LastPositionId = personnelView.LastPositionId,
                    MaritalStatusId = personnelView.MaritalStatusId,
                    PlaceOfBirth = personnelView.PlaceOfBirth,
                    ServiceLocationId = personnelView.ServiceLocationId,
                    StudyFieldId = personnelView.StudyFieldId,
                    TypeOfEmploymentId = personnelView.TypeOfEmploymentId,
                    Id = personnelView.Id
                };
                await _personnelComponent.UpdateAsync(model);
                return OperationResult.Success();
            }
            catch
            {
                return OperationResult.Error("مشکلی در عملیات پیش آمده");
            }
        }

        public async Task<List<ExcelViewModel>> ReadAsync()
        {
            var result = await _personnelComponent.ReadAsync();
            var model = await result.Select(q => new ExcelViewModel
            {
                BirthCertificateNumber = q.BirthCertificateNumber,
                BirthDate = q.BirthDate,
                CaseNumber = q.CaseNumber,
                CaseStatus = q.CaseStatus.Title,
                ComputerCode = q.ComputerCode,
                EducationDegree = q.EducationDegree.Title,
                Family = q.Family,
                FatherName = q.FatherName,
                LastPosition = q.TypeOfPosition.Title,
                MaritalStatus = q.MaritalStatus.Title,
                Name = q.Name,
                NationalCode = q.NationalCode,
                PlaceOfBirth = q.PlaceOfBirth,
                RegDate = q.RegDate.ToPersianDate(),
                ServiceLocation = q.ServiceLocation.Title,
                StudyField = q.StudyField.Title,
                TypeOfEmployment = q.TypeOfEmployment.Title,
            }).OrderBy(q => q.CaseStatus).ThenBy(q => q.CaseNumber).ToListAsync();
            return model;
        }

        public async Task<PersonnelFilterResult> ReadAsync(PersonnelFilterParams filterParams)
        {
            var result = await _personnelComponent.ReadAsync(filterParams);
            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var model = new PersonnelFilterResult
            {
                Data = await result.Select(q => new PersonnelViewModel
                {
                    BirthCertificateNumber = q.BirthCertificateNumber,
                    BirthDate = q.BirthDate,
                    CaseNumber = q.CaseNumber,
                    CaseStatusId = q.CaseStatusId,
                    ComputerCode = q.ComputerCode,
                    EducationDegreeId = q.EducationDegreeId,
                    Family = q.Family,
                    FatherName = q.FatherName,
                    Id = q.Id,
                    LastPositionId = q.LastPositionId,
                    MaritalStatusId = q.MaritalStatusId,
                    Name = q.Name,
                    NationalCode = q.NationalCode,
                    PlaceOfBirth = q.PlaceOfBirth,
                    RegDate = q.RegDate,
                    ServiceLocationId = q.ServiceLocationId,
                    StudyFieldId = q.StudyFieldId,
                    TypeOfEmploymentId = q.TypeOfEmploymentId,
                    ServiceLocationName = q.ServiceLocation.Title,
                    CaseStatusName = q.CaseStatus.Title
                }).OrderBy(q => q.CaseStatusId).ThenBy(q => q.CaseNumber).ToListAsync()
            };
            model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
            return model;

            //var result = await _personnelComponent.Read(personnelFilter);
            //var viewModel = result.Select(q => new PersonnelViewModel()
            //{
            //    Id = q.Id,
            //    BirthCertificateNumber = q.BirthCertificateNumber,
            //    BirthDate = q.BirthDate,
            //    NationalCode = q.NationalCode,
            //    CaseNumber = q.CaseNumber,
            //    CaseStatusId = q.CaseStatusId,
            //    ComputerCode = q.ComputerCode,
            //    DegreeOfEducationId = q.DegreeOfEducationId,
            //    Family = q.Family,
            //    FatherName = q.FatherName,
            //    Name = q.Name,
            //    LastPositionId = q.LastPositionId,
            //    MaritalStatusId = q.MaritalStatusId,
            //    PlaceOfBirth = q.PlaceOfBirth,
            //    ServiceLocationId = q.ServiceLocationId,
            //    StudyFieldId = q.StudyFieldId,
            //    TypeOfEmploymentId = q.TypeOfEmploymentId
            //}).ToList();
            //return new SysResult() { IsSuccess = true, Message = "اطلاعات با موفقیت واکشی شد", Value = viewModel };
        }

        public async Task<SysResult<PersonnelViewModel>> FindAsync(long id)
        {
            try
            {
                var result = await _personnelComponent.FindByIdAsync(id);
                if (result == null)
                    return new SysResult<PersonnelViewModel>() { IsSuccess = true, Message = "نام کاربری یافت نشد", Value = null };
                //**********************************************  عملیات نگاشت
                var viewModel = new PersonnelViewModel()
                {
                    Id = result.Id,
                    BirthCertificateNumber = result.BirthCertificateNumber,
                    BirthDate = result.BirthDate,
                    NationalCode = result.NationalCode,
                    CaseNumber = result.CaseNumber,
                    CaseStatusId = result.CaseStatusId,
                    ComputerCode = result.ComputerCode,
                    EducationDegreeId = result.EducationDegreeId,
                    Family = result.Family,
                    FatherName = result.FatherName,
                    Name = result.Name,
                    LastPositionId = result.LastPositionId,
                    MaritalStatusId = result.MaritalStatusId,
                    PlaceOfBirth = result.PlaceOfBirth,
                    ServiceLocationId = result.ServiceLocationId,
                    StudyFieldId = result.StudyFieldId,
                    TypeOfEmploymentId = result.TypeOfEmploymentId
                };
                //**********************************************
                return new SysResult<PersonnelViewModel>() { IsSuccess = true, Message = "اطلاعات با موفقیت واکشی شد", Value = viewModel };
            }
            catch (Exception m)
            {
                return new SysResult<PersonnelViewModel>() { IsSuccess = false, Message = m.Message };
            }
        }

        public async Task<OperationResult> ToggleStatusAsync(long id)
        {
            try
            {
                var personnel = await _personnelComponent.FindByIdAsync(id);
                var model = new PersonnelsModel()
                {
                    BirthCertificateNumber = personnel.BirthCertificateNumber,
                    BirthDate = personnel.BirthDate,
                    NationalCode = personnel.NationalCode,
                    CaseNumber = personnel.CaseNumber,
                    CaseStatusId = personnel.CaseStatusId,
                    ComputerCode = personnel.ComputerCode,
                    EducationDegreeId = personnel.EducationDegreeId,
                    Family = personnel.Family,
                    FatherName = personnel.FatherName,
                    Name = personnel.Name,
                    LastPositionId = personnel.LastPositionId,
                    MaritalStatusId = personnel.MaritalStatusId,
                    PlaceOfBirth = personnel.PlaceOfBirth,
                    ServiceLocationId = personnel.ServiceLocationId,
                    StudyFieldId = personnel.StudyFieldId,
                    TypeOfEmploymentId = personnel.TypeOfEmploymentId,
                    Id = personnel.Id
                };
                var result = await _personnelComponent.ToggleStatusAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
        }

        public async Task<SysResult> DeleteAllAsync(List<PersonnelViewModel> personnelViewModels)
        {
            //**********************************************  عملیات نگاشت
            var ids = personnelViewModels.Select(r => r.Id).ToList();
            await _personnelComponent.DeleteAllAsync(ids);
            //**********************************************
            return new SysResult() { IsSuccess = true, Message = "عملیات حذف با موفقیت انجام شد" };

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}