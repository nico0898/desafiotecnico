// src/CalendarView.jsx
import { useEffect, useState } from "react";
import axios from "axios";
import FullCalendar from "@fullcalendar/react";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";

const api = axios.create({
    baseURL: "http://localhost:5233/api/reservation"
});

const Calendar= ({date, refresh}) => {
  const [events, setEvents] = useState([]);

  const fetchReservations = async (dateStr) => {
    try {
      const res = await api.get(`/${dateStr}`);
      const data = res.data.map(r => ({
        title: `${r.customerName} (Salón ${r.roomId})`,
        start: `${r.date}T${r.startTime}`,
        end: `${r.date}T${r.endTime}`,
        allDay: false
      }));
      setEvents(data);
    } catch {
      setEvents([]);
    }
  };

  useEffect(() => {
    fetchReservations(date);
  }, [date, refresh]);

  return (
    <div className="container mt-4">
      <h3>Reservas - {date}</h3>
      <FullCalendar
        plugins={[timeGridPlugin, interactionPlugin]}
        initialView="timeGridDay"
        slotMinTime="09:00:00"
        slotMaxTime="18:00:00"
        events={events}
        allDaySlot={false}
        initialDate={date}
        nowIndicator
        height="auto"
        eventClick={() => {}}
        headerToolbar={{
            left: "",     
            center: "",
            right: "" 
          }}
      />
    </div>
  );
} 

export default Calendar;