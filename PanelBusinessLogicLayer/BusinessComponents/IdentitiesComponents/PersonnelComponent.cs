using Common;
using DataAccessLayer.ApplicationDatabase;
using DataAccessLayer.Repositories;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using PanelViewModel.Filters;
using PanelViewModel.IdentitiesViewModels;
using System.Xml.Linq;

namespace PanelBusinessLogicLayer.BusinessComponents.IdentitiesComponents
{
    public class PersonnelComponent : IDisposable
    {
        private readonly Repository<PersonnelsModel> _personelRepository;
        private readonly Repository<PersonnelsModel> _Repository;

        public PersonnelComponent()
        {
            _personelRepository = new Repository<PersonnelsModel>();
            _Repository = new Repository<PersonnelsModel>();
        }

        public async Task<IQueryable<PersonnelsModel>> ReadAsync()
        {
            var result = _personelRepository.SelectAllAsQuerable();
            return result;
        }

        public async Task<IQueryable<PersonnelsModel>> ReadAsync(PersonnelFilterParams filterParams)
        {
            var result = _personelRepository.SelectAllAsQuerable();

            if (filterParams.FullName != null)
            {
                result = result.Where(q => q.Name.Contains(filterParams.FullName) || q.Family.Contains(filterParams.FullName));
            }
            if (filterParams.NationalCode != null)
            {
                result = result.Where(q => q.NationalCode.Contains(filterParams.NationalCode));
            }
            if (filterParams.CaseNumber != null)
            {
                result = result.Where(q => q.CaseNumber == filterParams.CaseNumber);
            }
            if (filterParams.EmploymentTypeId != null)
            {
                result = result.Where(q => q.TypeOfEmploymentId == filterParams.EmploymentTypeId);
            }
            if (filterParams.EducationDegreeId != null)
            {
                result = result.Where(q => q.EducationDegreeId == filterParams.EducationDegreeId);
            }
            if (filterParams.CaseStatus != null)
            {
                result = result.Where(q => q.CaseStatusId == filterParams.CaseStatus.Value);
            }
            if (filterParams.ServiceLocation != null)
            {
                result = result.Where(q => q.ServiceLocation.Title == filterParams.ServiceLocation);
            }

            return result;
        }

        public async Task AddAsync(PersonnelsModel personnelsModel)
        {
            var personnels = _Repository.SelectAllAsQuerable();
            var query = await _personelRepository.FirstOrDefaultAsync(q => q.NationalCode == personnelsModel.NationalCode);
            var caseNumber = personnels.Where(q => q.CaseStatusId == 1).OrderBy(q => q.CaseNumber).Select(q => q.CaseNumber).LastOrDefault();
            if (query != null)
            {
                throw new Exception("کد ملی قبلا ثبت شده است");
            }
            var model = new PersonnelsModel()
            {
                BirthCertificateNumber = personnelsModel.BirthCertificateNumber,
                BirthDate = personnelsModel.BirthDate,
                NationalCode = personnelsModel.NationalCode,
                CaseNumber = caseNumber + 1,
                CaseStatusId = personnelsModel.CaseStatusId,
                ComputerCode = personnelsModel.ComputerCode,
                EducationDegreeId = personnelsModel.EducationDegreeId,
                Family = personnelsModel.Family,
                FatherName = personnelsModel.FatherName,
                Name = personnelsModel.Name,
                LastPositionId = personnelsModel.LastPositionId,
                MaritalStatusId = personnelsModel.MaritalStatusId,
                PlaceOfBirth = personnelsModel.PlaceOfBirth,
                ServiceLocationId = personnelsModel.ServiceLocationId,
                StudyFieldId = personnelsModel.StudyFieldId,
                TypeOfEmploymentId = personnelsModel.TypeOfEmploymentId
            };
            await _personelRepository.AddAsync(model);
            await _personelRepository.SaveChangesAsync();
        }

        public async Task AddExcelAsync(List<PersonnelsModel> personnelsModel)
        {
            for (int i = 0; i < personnelsModel.Count; i++)
            {
                var personnels = _Repository.SelectAllAsQuerable();
                var query = await _personelRepository.FirstOrDefaultAsync(q => q.NationalCode == personnelsModel[i].NationalCode);
                var caseNumber = await personnels.Where(q => q.CaseStatusId == 1).OrderBy(q => q.CaseNumber).Select(q => q.CaseNumber).LastOrDefaultAsync();
                if (query != null)
                {
                    throw new Exception($"کد ملی {query} قبلا ثبت شده است");
                }
                var model = new PersonnelsModel()
                {
                    BirthCertificateNumber = personnelsModel[i].BirthCertificateNumber,
                    BirthDate = personnelsModel[i].BirthDate,
                    NationalCode = personnelsModel[i].NationalCode,
                    CaseNumber = caseNumber + 1,
                    CaseStatusId = 1,
                    ComputerCode = personnelsModel[i].ComputerCode,
                    EducationDegreeId = personnelsModel[i].EducationDegreeId,
                    Family = personnelsModel[i].Family,
                    FatherName = personnelsModel[i].FatherName,
                    Name = personnelsModel[i].Name,
                    LastPositionId = personnelsModel[i].LastPositionId,
                    MaritalStatusId = personnelsModel[i].MaritalStatusId,
                    PlaceOfBirth = personnelsModel[i].PlaceOfBirth,
                    ServiceLocationId = personnelsModel[i].ServiceLocationId,
                    StudyFieldId = personnelsModel[i].StudyFieldId,
                    TypeOfEmploymentId = personnelsModel[i].TypeOfEmploymentId
                };
                await _personelRepository.AddAsync(model);
                await _personelRepository.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(PersonnelsModel personnelsModel)
        {
            var query = await _personelRepository.FirstOrDefaultAsync(q => q.NationalCode == personnelsModel.NationalCode);
            var result = await _personelRepository.SingleOrDefaultAsync(q => q.Id == personnelsModel.Id);
            if (result.NationalCode != personnelsModel.NationalCode)
            {
                if (query != null)
                {
                    throw new Exception("کد ملی قبلا ثبت شده است");
                }
            }
            result.BirthCertificateNumber = personnelsModel.BirthCertificateNumber;
            result.BirthDate = personnelsModel.BirthDate;
            result.NationalCode = personnelsModel.NationalCode;
            result.ComputerCode = personnelsModel.ComputerCode;
            result.EducationDegreeId = personnelsModel.EducationDegreeId;
            result.Family = personnelsModel.Family;
            result.FatherName = personnelsModel.FatherName;
            result.Name = personnelsModel.Name;
            result.LastPositionId = personnelsModel.LastPositionId;
            result.MaritalStatusId = personnelsModel.MaritalStatusId;
            result.PlaceOfBirth = personnelsModel.PlaceOfBirth;
            result.ServiceLocationId = personnelsModel.ServiceLocationId;
            result.StudyFieldId = personnelsModel.StudyFieldId;
            result.TypeOfEmploymentId = personnelsModel.TypeOfEmploymentId;
            _personelRepository.Update(result);
            await _personelRepository.SaveChangesAsync();
        }

        public async Task<PersonnelsModel> FindByIdAsync(long Id)
        {
            var result = await _personelRepository.SingleOrDefaultAsync(q => q.Id == Id);
            return result;
        }

        public async Task DeleteAllAsync(List<long> ids)
        {
            try
            {
                _personelRepository.Delete(q => ids.Contains(q.Id));
                await _personelRepository.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("اطلاعات به علت خطا حذف نشد");
            }
        }

        public async Task DeleteAsync(PersonnelsModel personnelsModel)
        {
            _personelRepository.Delete(personnelsModel);
            await _personelRepository.SaveChangesAsync();
        }

        public async Task<OperationResult> ToggleStatusAsync(PersonnelsModel personnelsModel)
        {
            var personnels = _personelRepository.SelectAllAsQuerable();
            var currentCase = await personnels.OrderBy(q => q.CaseNumber).Where(q => q.CaseStatusId == 1).Select(q => q.CaseNumber).ToListAsync();
            var stagnantCase = await personnels.OrderBy(q => q.CaseNumber).Where(q => q.CaseStatusId == 2).Select(q => q.CaseNumber).ToListAsync();
            long caseNumber = -1;
            if (personnelsModel.CaseStatusId == 1)
            {
                personnelsModel.CaseStatusId = 2;
                if (stagnantCase.Count == 0)
                {
                    caseNumber = 1;
                }
                else
                {
                    if (stagnantCase[0] != 1)
                    {
                        caseNumber = 1;
                    }
                    else
                    {
                        for (var i = 0; i < stagnantCase.Count; i++)
                        {
                            if (stagnantCase[i] != i + 1)
                            {
                                caseNumber = i + 1;
                                break;
                            }
                        }
                    }

                    if (caseNumber == -1)
                    {
                        caseNumber = stagnantCase.LastOrDefault() + 1;
                    }
                }
                personnelsModel.CaseNumber = caseNumber;
            }
            else
            {
                personnelsModel.CaseStatusId = 1;
                if (currentCase.Count == 0)
                {
                    caseNumber = 1;
                }
                else
                {
                    if (currentCase[0] != 1)
                    {
                        caseNumber = 1;
                    }
                    else
                    {
                        for (var i = 0; i < currentCase.Count; i++)
                        {
                            if (currentCase[i] != i + 1)
                            {
                                caseNumber = i + 1;
                                break;
                            }
                        }
                    }
                    if (caseNumber == -1)
                    {
                        caseNumber = currentCase.LastOrDefault() + 1;
                    }
                }
                personnelsModel.CaseNumber = caseNumber;
            }

            _Repository.Update(personnelsModel);
            await _Repository.SaveChangesAsync();
            return OperationResult.Success($"شماره پرونده جدید {caseNumber}");
        }

        public void Dispose()
        {
            _personelRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
