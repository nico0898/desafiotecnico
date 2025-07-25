import { useEffect, useState } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";

import Calendar from "./components/Calendar";

const api = axios.create({
  baseURL: "http://localhost:5233/api/reservation"
});

const App = () => {
  const formatTime = (t) => t.length === 5 ? `${t}:00` : t;

  const [message, setMessage] = useState("");
  const [refreshTrigger, setRefreshTrigger] = useState(false);

  const [form, setForm] = useState({
    roomId: 1,
    date: new Date().toISOString().split("T")[0],
    startTime: "09:00",
    endTime: "10:00",
    customerName: ""
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");
    
    const data = {
      ...form,
      startTime: formatTime(form.startTime),
      endTime: formatTime(form.endTime)
    };

    try {
      await api.post("", data);
      setMessage("Reserva creada exitosamente.");
      setRefreshTrigger(prev => !prev);
    } catch (err) {
      setMessage(err.response?.data?.message || "Error al crear la reserva.");
    }
  };

  return (
    <div className="container">
      <div className="row mt-4">
        <div className="col-md-6">
          <h4>Crear Reserva</h4>
          <form onSubmit={handleSubmit}>
            <div className="mb-2">
              <label>Fecha</label>
              <input type="date" name="date" className="form-control" value={form.date} onChange={handleChange} required />
            </div>
            <div className="mb-2">
              <label>Salón</label>
              <select name="roomId" className="form-select" value={form.roomId} onChange={handleChange}>
                {[1, 2, 3].map(id => <option key={id} value={id}>Salón {id}</option>)}
              </select>
            </div>
            <div className="mb-2">
              <label>Hora Inicio</label>
              <input type="time" name="startTime" className="form-control" value={form.startTime} onChange={handleChange} required />
            </div>
            <div className="mb-2">
              <label>Hora Fin</label>
              <input type="time" name="endTime" className="form-control" value={form.endTime} onChange={handleChange} required />
            </div>
            <div className="mb-2">
              <label>Nombre</label>
              <input type="text" name="customerName" className="form-control" value={form.customerName} onChange={handleChange} required />
            </div>
            <button type="submit" className="btn btn-primary">Reservar</button>
          </form>
          {message && <div className="alert alert-info mt-2">{message}</div>}
        </div>

        <div className="col-md-6">
          <Calendar date={form.date} refresh={refreshTrigger}/>
        </div>
      </div>
    </div>
  );
}

export default App;