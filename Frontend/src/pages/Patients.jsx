
import * as patientsService from "../services/patientsService";
import { useEffect, useState } from "react";
import CreateEditPatientModal from "../components/CreateEditPatientModal";
import Button from 'react-bootstrap/Button';
import Swal from 'sweetalert2';

const emptyForm = {
    name: '',
    egn: '',
    phoneNumber: '',
    address: '',
};

export default function Patients() {

    const [patients, setPatients] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [editIndex, setEditIndex] = useState(null);
    const [editId, setEditId] = useState(null);
    const [formData, setFormData] = useState(emptyForm);

    useEffect(() => {
        loadPatients();
    }, []);

    const loadPatients = async () => {
        try {
            const result = await patientsService.getAll();
            setPatients(result);
        } catch (err) {
            console.error(err);

            Swal.fire({
                icon: "error",
                title: "Грешка",
                text: err.message || "Неуспешно зареждане на пациентите."
            });
        }
    };

    const showm = (value) => {
        setShowModal(value);
    };

    const addPat = () => {
        setFormData({ ...emptyForm });
        setEditIndex(null);
        setEditId(null);
        setShowModal(true);
    };

    const editRecord = (patient, index) => {
        setFormData({
            name: patient.name,
            egn: patient.egn,
            phoneNumber: patient.phoneNumber || '',
            address: patient.address || '',
        });

        setEditIndex(index);
        setEditId(patient.id);
        setShowModal(true);
    };

    const handleAddOrEdit = async () => {
        try {
            if (editId !== null) {

                // Редактиране
                const updatedPatient = await patientsService.update(
                    editId,
                    formData
                );

                setPatients(prev =>
                    prev.map(patient =>
                        patient.id === editId
                            ? updatedPatient
                            : patient
                    )
                );

                Swal.fire({
                    icon: "success",
                    title: "Успешно",
                    text: "Данните на пациента са обновени."
                });

            } else {

                // Добавяне
                const newPatient = await patientsService.create(formData);

                setPatients(prev => [...prev, newPatient]);

                Swal.fire({
                    icon: "success",
                    title: "Успешно",
                    text: "Пациентът е добавен."
                });
            }

            setFormData({ ...emptyForm });
            setEditIndex(null);
            setEditId(null);
            setShowModal(false);

        } catch (err) {
            console.error(err);

            Swal.fire({
                icon: "error",
                title: "Грешка",
                text: err.message || "Възникна грешка при запис."
            });
        }
    };

    const handleDelete = async (id) => {

        const result = await Swal.fire({
            title: "Изтриване на запис!",
            text: "Сигурни ли сте?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да, изтриване",
            cancelButtonText: "Отказ"
        });

        if (!result.isConfirmed) {
            return;
        }

        try {
            await patientsService.del(id);

            setPatients(prev =>
                prev.filter(patient => patient.id !== id)
            );

            Swal.fire({
                title: "Изтриване!",
                text: "Избраният запис беше изтрит.",
                icon: "success"
            });

        } catch (err) {
            console.error(err);

            Swal.fire({
                icon: "error",
                title: "Грешка",
                text: err.message || "Неуспешно изтриване."
            });
        }
    };

    return (
        <main id="main" className="main">

            <div className="pagetitle">
                <h1>Списък на пациентите</h1>

                <nav>
                    <ol className="breadcrumb">
                        <li className="breadcrumb-item">
                            <a href="/">Начало</a>
                        </li>
                        <li className="breadcrumb-item active">
                            Пациенти
                        </li>
                    </ol>
                </nav>
            </div>

            <section className="section alist">
                <div className="card">
                    <div className="card-body">

                        <div className="mt-3">
                            <table className="table table-striped table-hover w-100">

                                <thead>
                                    <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">Имена</th>
                                        <th scope="col">ЕГН/ЛНЧ</th>
                                        <th scope="col">ТЕЛЕФОН</th>
                                        <th scope="col">АДРЕС</th>
                                        <th scope="col">Действия</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    {patients.map((patient, index) => (
                                        <tr key={patient.id}>
                                            <td>{index + 1}</td>
                                            <td>{patient.name}</td>
                                            <td>{patient.egn}</td>
                                            <td>{patient.phoneNumber}</td>
                                            <td>{patient.address}</td>

                                            <td>
                                                <i
                                                    className="bi bi-pencil-square edit_but"
                                                    onClick={() => editRecord(patient, index)}
                                                />

                                                <i
                                                    className="bi bi-x-square del_but"
                                                    onClick={() => handleDelete(patient.id)}
                                                />
                                            </td>
                                        </tr>
                                    ))}

                                    {patients.length === 0 && (
                                        <tr>
                                            <td colSpan="6" className="text-center">
                                                Няма регистрирани пациенти.
                                            </td>
                                        </tr>
                                    )}
                                </tbody>

                            </table>
                        </div>

                    </div>
                </div>
            </section>

            <CreateEditPatientModal
                showModal={showModal}
                showm={showm}
                editIndex={editIndex}
                formData={formData}
                setFormData={setFormData}
                handleAddOrEdit={handleAddOrEdit}
            />

            <Button variant="primary" onClick={addPat}>
                Нов пациент
            </Button>

        </main>
    );
}
