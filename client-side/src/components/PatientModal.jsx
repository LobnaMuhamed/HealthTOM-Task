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

const PatientModal = ({ open, onClose, patientData, onSave }) => {
  const [patient, setPatient] = useState({
    fullName: "",
    email: "",
    gender: "",
    birthDate: "",
    ...patientData,
  });

  useEffect(() => {
    if (open) setPatient({ ...patientData});
  }, [patientData, open]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setPatient((prev) => ({ ...prev, [name]: value }));
  };

  const handleSave = () => {
    const formattedPatient = {
      ...patient,
      gender: patient.gender === "M" ? true : false,
    };
    onSave(formattedPatient);
  };

  return (
    <Modal
      open={open}
      onClose={onClose}
      aria-labelledby="patient-modal-title"
      aria-describedby="patient-modal-description"
    >
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
        <Typography id = "patient-modal-title" variant="h6" gutterBottom>
          {patient?.patientID ? "Update Patient" : "Add New Patient"}
        </Typography>
        <Stack spacing={2}>
          <TextField
            label="Full Name"
            name="fullName"
            value={patient.fullName || ""}
            onChange={handleChange}
            fullWidth
            required
          />

          <TextField
            label="Email"
            name="email"
            type="email"
            value={patient.email || ""}
            onChange={handleChange}
            fullWidth
            required
          />
          <FormControl fullWidth required>
            <InputLabel id="gender-id">Gender</InputLabel>
            <Select
              labelId="gender-label"
              name="gender"
              value={patient.gender || ""}
              onChange={handleChange}
            >
              <MenuItem value="M">Male</MenuItem>
              <MenuItem value="F">Female</MenuItem>
            </Select>
          </FormControl>
          <TextField
            label="BirthDate"
            name="birthDate"
            type="date"
            value={patient.birthDate || ""}
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

export default PatientModal;
