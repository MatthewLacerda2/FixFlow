import LogsTable from "../../../Components/PagesComponents/MainPage/BusinessPage/LogsPage/LogsTable/LogsTable";
import BusinessSidebar from "../../../Components/Sidebar/BusinessSidebar/BusinessSidebar";
import { AptLog } from "../../../FlowApi";
import "../../../Styles/Form.css";

const LogsPage: React.FC = () => {
  const logs: AptLog[] = [
    {
      id: "1",
      clientId: "c1",
      client: { id: "c1", fullName: "John Doe" },
      businessId: "b1",
      dateTime: "2024-05-16T10:00:00Z",
      price: 100,
      observation: "Initial consultation",
    },
  ];
  return (
    <div className="user-page">
      <BusinessSidebar />
      <div className="form-group">
        <input
          className="input-field "
          type="text"
          placeholder="Nome do Cliente"
        ></input>
        <input
          className="input-field"
          type="text"
          placeholder="Observação"
        ></input>
        <input
          className="input-field"
          style={{ width: "150px" }}
          type="date"
        ></input>
        {/*min date*/}
        <input
          className="input-field"
          style={{ width: "150px" }}
          type="date"
        ></input>
        {/*max date*/}
        <input
          className="input-field"
          style={{ width: "120px" }}
          type="number"
          placeholder="Preço mínimo"
        ></input>
        <input
          className="input-field"
          style={{ width: "120px" }}
          type="number"
          placeholder="Preço maximo"
        ></input>
      </div>
      <LogsTable logs={logs}></LogsTable>
    </div>
  );
};
export default LogsPage;
