import moment from "moment";
import PAGES from "navigation/pages";
import { useRef } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router";
import { RootState } from "stores";

const useJobApplication = () => {
  const navigate = useNavigate();
  const agRef = useRef(null);

  const columnDefs = [
    {
      field: "jobTitle",
      headerName: "Job Title"
    },
    {
      field: "firstName",
      headerName: "First Name",
    },
    {
      field: "lastName",
      headerName: "Last Name",
    },
    {
      field: "appliedDate",
      headerName: "Applied Date",
      cellRenderer: (params: any) => {
        return moment(params?.data).format("MMM DD, YYYY");
      },
    },
  ];

  const jobApplications = useSelector((state: RootState) => state.jobApplication.jobApplications);

  const handleCreateManuallyJobApplication = () => {
    navigate(PAGES.newJobApplication);
  };

  const handleEditJobApplication = (id: string) => {
    navigate(`${PAGES.editJobApplication}/${id}`);
  };

  return {
    // state
    agRef,
    columnDefs,
    jobApplications,

    // funcs
    handleCreateManuallyJobApplication,
    handleEditJobApplication,
  };
};

export default useJobApplication;
