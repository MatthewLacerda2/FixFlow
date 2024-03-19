import Card from '../../Components/Card/Card';
import ScheduleFormulary from '../../Components/ScheduleFormulary';

function CreateSchedulePage() {
  return (
    <Card  
      title="Create Schedule Formulary"
      items={[<ScheduleFormulary />]}
    />
  );
}

export default CreateSchedulePage;