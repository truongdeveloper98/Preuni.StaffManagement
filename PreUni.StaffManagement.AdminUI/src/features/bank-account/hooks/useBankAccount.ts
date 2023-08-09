import { useEffect, useState } from "react";
import { getBankAccountByJobApplicationId, updateBankAccount } from "../bankAccount.service";
import { useNavigate, useParams } from "react-router-dom";
import * as Yup from "yup";
import PAGES from "navigation/pages";

interface FormValues {
  bankName: string;
  accountHolderName: string;
  BSBNumber: number;
  accountNumber: number;
  emailAddress: string;
  phoneNumber: string;
  nameApplicant: string;
  signature: string;
  signDate: string;
}

const useBankAccount = () => {
  const [bankAccountDetail, setBankAccountDetail] = useState(undefined);
  const params = useParams();
  const { id: paramId } = params;
  const navigate = useNavigate();

  const initialValues: FormValues = {
    bankName: bankAccountDetail?.data?.bankName,
    accountHolderName: bankAccountDetail?.data?.accountHolderName,
    BSBNumber: bankAccountDetail?.data?.bsbNumber,
    accountNumber: bankAccountDetail?.data?.accountNumber,
    emailAddress: bankAccountDetail?.data?.emailAddress,
    phoneNumber: bankAccountDetail?.data?.phoneNumber,
    nameApplicant: bankAccountDetail?.data?.nameApplicant,
    signature: bankAccountDetail?.data?.signature,
    signDate: bankAccountDetail?.data?.signDate,
  };

  const bankAccountSchema = Yup.object().shape({
    bankName: Yup.string().required("Bank name is required"),
    accountHolderName: Yup.string().required("Account holder name is required"),
    BSBNumber: Yup.number().required("BSB number is required"),
    accountNumber: Yup.number().required("Account number is required"),
    emailAddress: Yup.string().email("Invalid email").required("Email is required"),
    phoneNumber: Yup.string().required("Phone number is required"),
    nameApplicant: Yup.string().required("Applicant name is required"),
    signature: Yup.string().required("Signature is required"),
    signDate: Yup.string().required("Sign date is required"),
  });

  const handleSubmitForm = (data: any) => {
    if (paramId) {
      updateBankAccount(data, Number(paramId), (data: any) => {
        console.log({ data });
      });
    }
  };

  const handleCancel = () => {
    navigate(PAGES.jobApplication);
  };

  useEffect(() => {
    if (paramId) {
      getBankAccountByJobApplicationId(Number(paramId), (data: any) => {
        setBankAccountDetail(data);
      });
    }
  }, [paramId]);

  return {
    initialValues,
    bankAccountSchema,
    handleSubmitForm,
    handleCancel,
  };
};

export default useBankAccount;
