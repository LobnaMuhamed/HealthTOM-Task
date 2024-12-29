import React, { useState, useEffect } from "react";
import { createAPIEndpoint, EndPoints } from "../API/index";
import { DataGrid } from "@mui/x-data-grid";
import { Grid2, IconButton, Stack, Typography } from "@mui/material";
import { Add, Delete, Edit } from "@mui/icons-material";
import PatientModal from "./PatientModal";

const PatientList = () => {
  const [patients, setPatients] = useState([]);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [selectedPatient, setSelectedPatient] = useState(null);

  useEffect(() => {
    fetchPatients();
  }, []);
  
  const fetchPatients = async () => {
    setLoading(true);

    createAPIEndpoint(EndPoints.Patient.GetAllPatients)
      .fetchAll()
      .then((response) => {
        setPatients(response.data);
        setLoading(false);
      })
      .catch((err) => {
        console.error("Error fetching patients:", err);
        setLoading(false);
      });
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this patient?"))
      createAPIEndpoint(EndPoints.Patient.DeletePatient)
        .delete(id)
        .then(() => {
          setPatients((prevPatient) =>
            prevPatient.filter((patient) => patient.patientID !== id)
          );
        })
        .catch((error) => {
          console.error("Error deleting patient:", error);
          setLoading(false);
        });
  };

  const handleOpenModel = (patient = null) => {
    setSelectedPatient(patient);
    setModalOpen(true);
  };

  const handleCloseModal = () => {
    setSelectedPatient(null);
    setModalOpen(false);
  };

  const handleSavePatient = (patient) => {
    const isNew = !patient.patientID;
    const endpoint = isNew
      ? EndPoints.Patient.AddNewPatient
      : EndPoints.Patient.UpdatePatient;
    const action = isNew ? "post" : "put";

    createAPIEndpoint(endpoint)
      [action](isNew ? patient : patient.patientID, patient)
      .then(() => {
        fetchPatients();
        handleCloseModal();
      })
      .catch((error) => {
        console.error("Error saving patient: ", error);
      });

      console.log(patient);
  };

  const columns = [
    { field: "patientID", headerName: "Patient-ID", width: 200 },
    { field: "fullName", headerName: "Patient-Name", width: 200 },
    { field: "email", headerName: "Email", width: 200 },
    { field: "birthDate", headerName: "Birthdate", width: 200 },
    { field: "gender", headerName: "Gender", width: 200 },
    {
      field: "action",
      headerName: "Action",
      width: 100,
      renderCell: (params) => (
        <Stack direction="row">
          <IconButton onClick={() => handleOpenModel(params.row)}>
            <Edit />
          </IconButton>

          <IconButton onClick={() => handleDelete(params.row.patientID)}>
            <Delete />
          </IconButton>
        </Stack>
      ),
    },
  ];

  return (
    <div style={{ height: 400, width: "100%" }}>
      <Grid2 sx={{ m: 1 }}>
        <Typography variant="h4" textAlign="center">
          Patients List
        </Typography>
        <IconButton onClick={() => handleOpenModel()}>
          <Add fontSize="large" color="primary" />
        </IconButton>
      </Grid2>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <DataGrid
          rows={patients}
          columns={columns}
          pageSize={5}
          getRowId={(row) => row.patientID}
        />
      )}

      <PatientModal
        open={modalOpen}
        onClose={handleCloseModal}
        patientData={selectedPatient}
        onSave={handleSavePatient}
      />
    </div>
  );
};

export default PatientList;
