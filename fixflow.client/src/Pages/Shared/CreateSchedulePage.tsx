import Card from '../../Components/Card';
import ScheduleFormulary from '../../Components/ScheduleFormulary';

function CreateSchedulePage() {

  const generateScheduleFormulary = () => {
    const logs = [];
    logs.push(<ScheduleFormulary/>);
    return logs;
  };
  
  return (
    <Card  
      title="Create Schedule Formulary"
      items={generateScheduleFormulary()}
    />
  );
}

export default CreateSchedulePage;