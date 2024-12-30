import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  Modal,
  Stack,
  TextField,
  Typography,
  MenuItem,
  Select,
  InputLabel,
  FormControl,
} from "@mui/material";
import { createAPIEndpoint, EndPoints } from "../API";

const ExamModal = ({ open, onClose, examData, onSave }) => {
  const [exam, setExam] = useState(examData || {});
  const [examTypes, setExamTypes] = useState([]);
  const [examStatuses, setExamStatuses] = useState([]);
  const [patients, setPatients] = useState([]);

  useEffect(() => {
    setExam(examData || {});
  }, [examData]);

  const fetchPatients = async () => {
    try {
      const response = await createAPIEndpoint(
        EndPoints.Patient.GetAllPatients
      ).fetchAll();
      setPatients(response.data);
    } catch (error) {
      console.error("Error fetching patients: ", error);
    }
  };
  useEffect(() => {
    const fetchExamTypes = async () => {
      if (examTypes.length) return;
      try {
        const response = await createAPIEndpoint(
          EndPoints.Exam.GetAllTypes
        ).fetchAll();
        setExamTypes(response.data);
      } catch (error) {
        console.error("Error fetching exam types:", error);
      }
    };

    const fetchExamStatuses = async () => {
      if (examStatuses.length) return;
      try {
        const response = await createAPIEndpoint(
          EndPoints.Exam.GetAllStatues
        ).fetchAll();
        setExamStatuses(response.data);
      } catch (error) {
        console.error("Error fetching exam statuses:", error);
      }
    };

    fetchExamTypes();
    fetchExamStatuses();
    fetchPatients();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setExam((prev) => ({ ...prev, [name]: value }));
  };

  const handleSave = () => {
    onSave(exam);
  };

  return (
    <Modal open={open} onClose={onClose}>
      <Box
        sx={{
          position: "absolute",
          top: "50%",
          left: "50%",
          transform: "translate(-50%, -50%)",
          width: 400,
          bgcolor: "background.paper",
          boxShadow: 24,
          p: 4,
          borderRadius: 2,
        }}
      >
        <Typography variant="h6" gutterBottom>
          {exam?.id ? "Update Exam" : "Add New Exam"}
        </Typography>
        <Stack spacing={2}>
          <FormControl fullWidth>
            <InputLabel id="patient-id-label">Patient ID</InputLabel>
            <Select
              labelId="patient-id-label"
              name="patientID"
              value={exam.patientID || ""}
              onChange={handleChange}
            >
              {patients.map((patient) => (
                <MenuItem key={patient.patientID} value={patient.patientID}>
                  {patient.patientID}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <FormControl fullWidth>
            <InputLabel id="exam-type-label">Exam Type</InputLabel>
            <Select
              labelId="exam-type-label"
              name="examTypeID"
              value={exam.examTypeID || ""}
              onChange={handleChange}
            >
              {examTypes.map((type) => (
                <MenuItem key={type.id} value={type.id}>
                  {type.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <FormControl fullWidth>
            <InputLabel id="exam-status-label">Exam Status</InputLabel>
            <Select
              labelId="exam-status-label"
              name="examStatusID"
              value={exam.examStatusID || ""}
              onChange={handleChange}
            >
              {examStatuses.map((status) => (
                <MenuItem key={status.id} value={status.id}>
                  {status.statusName}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <TextField
            label="Comment"
            name="comments"
            value={exam.comments || ""}
            onChange={handleChange}
            fullWidth
          />
          <TextField
            label="Execute Date"
            name="executeDate"
            type="date"
            value={exam.executeDate || ""}
            onChange={handleChange}
            fullWidth
            InputLabelProps={{ shrink: true }}
          />
          <Button variant="contained" onClick={handleSave}>
            Save
          </Button>
        </Stack>
      </Box>
    </Modal>
  );
};

export default ExamModal;
