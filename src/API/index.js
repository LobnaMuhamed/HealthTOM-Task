import axios from "axios";

export const BASE_URL = "https://localhost:7164/api/";

export const EndPoints = {
  Account: {
    register: "Account/register",
    login: "Account/login",
    detail: "Account/detail",
  },
  Exam: {
    GetAllExams: "Exam/all",
    GetExamByDate: "Exam/by-date/",
    GetExamByID: "Exam/by-id/",
    GetExamByName: "Exam/by-name/",
    GetExamsByPatientID: "Exam/by-patient-id/",
    GetAllTypes: "Exam/get-all-types",
    GetAllStatues: "Exam/get-all-status",
    GetExamsByPatientName: "Exam/by-patient-name/",
    AddNewExam: "Exam/add-new-exam/",
    UpdateExam: "Exam/update-exam/",
    DeleteExam: "Exam/delete-exam/",

  },
  Patient: {
    GetAllPatients: "Patient/all",
    GetPatientById: "Patient/by-id/",
    AddNewPatient: "Patient/add-new-patient",
    UpdatePatient: "Patient/update-patient/",
    DeletePatient: "Patient/delete-patient/",
  },
};

export const createAPIEndpoint = (endpoint) => {
  let url = BASE_URL + endpoint;
 const getAuthToken = () => localStorage.getItem("token");

 const getConfig = () => {
   const token = getAuthToken();
   return token
     ? {
         headers: {
           Authorization: `Bearer ${token}`, 
         },
       }
     : {};
 };


  return {
    fetchAll: () => axios.get(url, getConfig()),
    fetchBy: (id) => axios.get(url + id, getConfig()),
    post: (newRecord) => axios.post(url, newRecord, getConfig()),
    put: (id, updatedRecord) => axios.put(url + id, updatedRecord, getConfig()),
    delete: (id) => axios.delete(url + id, getConfig()),
  };
};
