import AgGridTable from "components/Customized/AgGridTable";
import BaseLayout from "components/Customized/BaseLayout";
import useJobApplication from "./hooks/useJobApplication";
import { getJobApplicationsRequest } from "./jobApplication.service";

const JobApplication = () => {
  const {
    agRef,
    columnDefs,
    jobApplications,
    handleCreateManuallyJobApplication,
    handleEditJobApplication,
  } = useJobApplication();

  return (
    <BaseLayout>
      <AgGridTable
        ref={agRef}
        columnDefs={columnDefs}
        entity={jobApplications}
        onFetching={getJobApplicationsRequest}
        onEdit={({ id }) => handleEditJobApplication(id)}
        onCreate={handleCreateManuallyJobApplication}
        entityName="Job Application"
      />
    </BaseLayout>
  );
};

export default JobApplication;
