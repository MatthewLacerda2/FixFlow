namespace Server.Models;

public class TimeInterval {

    public DateTime Start { get; set; }
    public DateTime Finish { get; set; }

    public override string ToString() {
        return interval;
    }

    public string interval {
        get {
            CheckOrder();
            return Start.ToString() + "-" + Finish.ToString();
        }
    }

    public TimeSpan Duration{
        get {
            CheckOrder();
            return Finish - Start; // Return the time span between Start and Finish
        }
    }

    void CheckOrder() {
        if (Finish < Start) {
            DateTime aux = Start;
            Start = Finish;
            Finish = aux;
        }
    }
}