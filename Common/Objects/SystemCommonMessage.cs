using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Common.Objects
{
    public static class SystemCommonMessage
    {
 
        public static string LoginFaild { get; set;  } = "نام کاربری یا رمز عبور صحیح نمی باشد";
        public static string InformationWasSuccessfullyRecorded { get; set; } = "اطلاعات با موفقیت ثبت شد";
        public static string OperationDoneSuccessfully { get; set; } = "عملیات با موفقیت انجام شد";
        public static string OperationStoppedByError { get; set; } = "عملیات با خطا متوقف شد";
        public static string InformationFetchedSuccessfully { get; set; } = "اطلاعات با موفقیت واکشی شد";
        public static string InformationWasSuccessfullyEdited { get; set; } = "اطلاعات با موفقیت ویرایش شد";
        public static string InformationWasSuccessfullyDeleted { get; set; } = "اطلاعات با موفقیت حذف شد";
        public static string InputDataIsIncorrect { get; set; } = "داده های ورودی صحیح نمی باشد";
        public static string DataWasNotFound { get; set; } = "داده مورد نظر یافت نشد";
        public static string NoAccessToThisSection { get; set; } = "شما مجوز دسترسی به این بخش را ندارید";
        public static string NotAllowedToPerformThisOperation { get; set; } = "مجاز به انجام این عملیات نمی باشید";
        public static string IdentifierIsNotValid { get; set; } = "شناسه ورودی معتبر نمی باشد";
        public static string NotEnoughInventory { get; set; } // "موجودی کافی نیست";
        public static string InvoiceNotFound { get; set; } // "صورتحساب یافت نشد";
        public static string DataWasNotRecordedDueToAnError { get; set; } // "اطلاعات به علت خطا ثبت نشد";
        public static string AmountEnteredIsNotCorrect { get; set; } // "مبلغ وارد شده صحیح نمی باشد";

    }
}
