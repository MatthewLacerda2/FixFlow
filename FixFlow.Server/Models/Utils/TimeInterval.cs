namespace Server.Models;

public class TimeInterval {

    public DateTime start, finish;

    public override string ToString() {
        return interval;
    }

    public string interval {
        get {
            CheckOrder();
            return start.ToString() + "-" + finish.ToString();
        }
    }

    public DateTime duration {
        get {
            CheckOrder();
            return DateTime.MinValue + (finish-start);
        }
    }

    void CheckOrder(){
        if(finish < start){
            DateTime aux = start;
            start=finish;
            finish=aux;
        }
    }
}