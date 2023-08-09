import { useEffect, useState } from "react";
import {
  getTFNDeclareByJobApplicationId,
  updateTFNDeclareByJobApplicationId,
} from "../tFNDeclare.service";
import { useNavigate, useParams } from "react-router-dom";
import * as Yup from "yup";
import PAGES from "navigation/pages";
import { WorkType } from "../constants";

interface FormValues {
  taxNumber: string;
  nameTitle: string;
  surName: string;
  firstName: string;
  otherName?: string;
  taxNumberOption: string;
  previousLastName?: string;
  address: string;
  suburb: string;
  state: string;
  postCode: string;
  workType: string;
  isAustraliaResident: boolean;
  isTaxFee: boolean;
  isTaxOffsetForPensioners: boolean;
  isEducationLoan: boolean;
  isFinancialSupplementSupport: boolean;
  signature: string;
  signDate: string;
  dayOfBirth: number;
  monthOfBirth: number;
  yearOfBirth: number;
}

const useTFNDeclare = () => {
  const [tfnDeclare, setTFNDeclare] = useState(undefined);
  const params = useParams();
  const { id: paramId } = params;
  const navigate = useNavigate();

  const initialValues: FormValues = {
    taxNumber: tfnDeclare?.data?.taxNumber,
    nameTitle: tfnDeclare?.data?.nameTitle,
    surName: tfnDeclare?.data?.surname,
    firstName: tfnDeclare?.data?.firstName,
    otherName: tfnDeclare?.data?.otherName,
    taxNumberOption: tfnDeclare?.data?.taxNumberOption,
    previousLastName: tfnDeclare?.data?.previousLastName,
    address: tfnDeclare?.data?.address,
    suburb: tfnDeclare?.data?.suburb,
    state: tfnDeclare?.data?.state,
    postCode: tfnDeclare?.data?.postCode,
    workType: tfnDeclare?.data?.workType || WorkType[0].value,
    isAustraliaResident: tfnDeclare?.data?.isAustraliaResident,
    isTaxFee: tfnDeclare?.data?.isTaxFee,
    isTaxOffsetForPensioners: tfnDeclare?.data?.isTaxOffsetForPensioners,
    isEducationLoan: tfnDeclare?.data?.isEducationLoan,
    isFinancialSupplementSupport: tfnDeclare?.data?.isFinancialSupplementSupport,
    signature: tfnDeclare?.data?.signature,
    signDate: tfnDeclare?.data?.signDate,
    dayOfBirth: tfnDeclare?.data?.dayOfBirth,
    monthOfBirth: tfnDeclare?.data?.monthOfBirth,
    yearOfBirth: tfnDeclare?.data?.yearOfBirth,
  };

  const bankAccountSchema = Yup.object().shape({
    taxNumber: Yup.string().required("Fax number is required"),
    nameTitle: Yup.string().required("Name title is required"),
    surName: Yup.string().required("Surname is required"),
    firstName: Yup.string().required("First name is required"),
    taxNumberOption: Yup.string().required("Tax number is required"),
    address: Yup.string().required("Address is required"),
    suburb: Yup.string().required("Suburb is required"),
    state: Yup.string().required("state is required"),
    postCode: Yup.string().required("Post code is required"),
    signature: Yup.string().required("Signature is required"),
    signDate: Yup.string().required("Sign date is required"),
    dayOfBirth: Yup.number().required("Day of birth is required"),
    monthOfBirth: Yup.number().required("Month of birth is required"),
    yearOfBirth: Yup.number().required("Year of birth is required"),
  });

  const handleSubmitForm = (data: any) => {
    if (paramId) {
      const newData = {
        ...data,
        jobApplicationId: Number(paramId),
      };

      updateTFNDeclareByJobApplicationId(newData, Number(paramId), (data: any) => {
        console.log({ data });
      });
    }
  };

  const handleCancel = () => {
    navigate(PAGES.jobApplication);
  };

  useEffect(() => {
    if (paramId) {
      getTFNDeclareByJobApplicationId(Number(paramId), (data: any) => {
        setTFNDeclare(data);
      });
    }
  }, [paramId]);

  return { initialValues, bankAccountSchema, handleSubmitForm, handleCancel };
};

export default useTFNDeclare;
