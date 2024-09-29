using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;


public class PopupEvent
{
    public string id;
    public string title;
    //public Sprite picture;
    public string description;
    public List<Choice> choices;
    public PopupEvent(string id, string title, string description, List<Choice> choices)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.choices = choices;
    }
}

public class PopupEventList
{
    private static PopupEventList instance;
    public List<PopupEvent> events;

    private PopupEventList()
    {
        events = new List<PopupEvent>();
        LoadEvents();
    }

    public static PopupEventList GetInstance()
    {
        if (instance == null)
            instance = new PopupEventList();
        return instance;
    }

    public void LoadEvents()
    {
        events.Add(new PopupEvent(
            "covid",

            "COVID Outbreak!",
            "A sudden outbreak of COVID has hit the city, overwhelming hospitals and requiring immediate resource allocation.",
            new List<Choice> {
                new Choice(
                    "Provide emergency medical funding!",
                    new List<ChoiceEffect> {
                        new ChoiceEffectAdd_f("money", -50000), // Ϊ����ҽ�Ʒ����ʽ�
                        new ChoiceEffectAdd_i("happiness", -10), // �Ҹ�ָ����ֻź͸����ʩ�½�
                        new ChoiceEffectAdd_i("healthRate", 25), // ҽ��ϵͳ��ǿ�����������
                        new ChoiceEffectAdd_i("population", -1000) // �����˿�������ʧȥ
                    }
                ),
                new Choice(
                    "Impose strict lockdown and wait for the outbreak to subside.",
                    new List<ChoiceEffect> {
                        new ChoiceEffectAdd_i("happiness", -30), // �ϸ���������Ҹ��д���½�
                        new ChoiceEffectAdd_i("crimeRate", 5), // ���������ֻź���᲻�ȶ�������������
                        new ChoiceEffectAdd_i("healthRate", 10), // ������Ч�������飬��������΢����
                        new ChoiceEffectAdd_i("population", -500) // ��Ȼ�в����˿�������ʧȥ
                    }
                )
            }
        ));
        events.Add(new PopupEvent(
            "harris",

            "Harris was shot by a bullet",
            "On *****, Donald Trump was shot...",
            new List<Choice> {
                new Choice(
                    "Raise your hand!",
                    new List<ChoiceEffect> {
                        new ChoiceEffectAdd_i("crimeRate", 1)
                    }
                ),
                new Choice(
                    "Flee!",
                    new List<ChoiceEffect> {
                        new ChoiceEffectAdd_i("healthRate", 1)
                    }
                )
            }
        ));
    }

    public PopupEvent GetEventById(string id)
    {
        return events.Find(e => e.id == id);
    }
}