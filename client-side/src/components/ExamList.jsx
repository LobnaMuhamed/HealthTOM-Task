import React, { useState, useEffect } from "react";
import { createAPIEndpoint, EndPoints } from "../API/index";

import { DataGrid } from "@mui/x-data-grid";
import { Grid2, IconButton, Stack, Typography } from "@mui/material";
import { Add, Delete, Edit } from "@mui/icons-material";
import ExamModal from "./ExamModal";

const ExamList = () => {
  const [exams, setExams] = useState([]);
  const [loading, setLoading] = useState(true);
  const [modalOpen, setModalOpen] = useState(false);
  const [selectedExam, setSelectedExam] = useState(null);


  useEffect(() => {
    fetchExams();
  }, []);

  const fetchExams = async () => {
   await createAPIEndpoint(EndPoints.Exam.GetAllExams)
      .fetchAll()
      .then((response) => {
        console.log(response.data);
        setExams(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching exams:", error);
        setLoading(false);
      });
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this exam?"))
      await createAPIEndpoint(EndPoints.Exam.DeleteExam)
        .delete(id)
        .then(() => {
          setExams((prevExams) => prevExams.filter((exam) => exam.id !== id));
        })
        .catch((error) => {
          console.error("Error deleting exam:", error);
        });
  };

  const handleOpenModel = (exam = null) => {
    setSelectedExam(exam);
    setModalOpen(true);
  };

  const handleCloseModal = () => {
    setSelectedExam(null);
    setModalOpen(false);
  };

  const handleSaveExam = async (exam) => {
    const isNew = !exam.id;
    const endpoint = isNew
      ? EndPoints.Exam.AddNewExam
      : EndPoints.Exam.UpdateExam;
    const action = isNew ? "post" : "put";

    await createAPIEndpoint(endpoint)
      [action](isNew ? exam : exam.id, exam)
      .then(() => {
        fetchExams();
        handleCloseModal();
      })
      .catch((error) => {
        console.error("Error saving exam: ", error);
      });
      console.log(exam);
  };

  const columns = [
    { field: "id", headerName: "Exam-ID", width: 70 },
    { field: "patientID", headerName: "Patient-ID", width: 70 },
    { field: "fullName", headerName: "Patient-Name", width: 100 },
    { field: "email", headerName: "Email", width: 100 },
    { field: "birthDate", headerName: "Birthdate", width: 100 },
    { field: "gender", headerName: "Gender", width: 70 },
    { field: "examTypeName", headerName: "Exam-Name", width: 100 },
    { field: "executeDate", headerName: "Exam-Date", width: 100 },
    { field: "statusName", headerName: "Status", width: 100 },
    { field: "comments", headerName: "Comments", width: 120 },
    { field: "userName", headerName: "Created By", width: 100 },
    {
      field: "action",
      headerName: "Action",
      width: 100,
      renderCell: (params) => (
        <Stack direction="row">
          <IconButton onClick={() => handleOpenModel(params.row)}>
            <Edit />
          </IconButton>

          <IconButton onClick={() => handleDelete(params.row.id)}>
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
          Exam List
        </Typography>
        <IconButton onClick={() => handleOpenModel()}>
          <Add fontSize="large" color="primary" />
        </IconButton>
      </Grid2>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <DataGrid rows={exams} columns={columns} pageSize={5} />
      )}

      <ExamModal
        open={modalOpen}
        onClose={handleCloseModal}
        examData={selectedExam}
        onSave={handleSaveExam}
      />
    </div>
  );
};

export default ExamList;
