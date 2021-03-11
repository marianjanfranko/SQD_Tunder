﻿using System.Collections.Generic;
using UnityEngine;
using Data;
using DG;
using DG.UML;
using Data.MOF;
using MongoDB.Bson;

public class LifelineFactory : AbstractFactory
{
    public GameObject PrefabLifeline = null;

    public override void RegisterFactory(FactoryRegister register)
    {
        register.RegisterXmiString("uml:Lifeline", this);
        register.RegisterClass(typeof(UML.Interactions.Lifeline), this);
        register.RegisterClass(typeof(DG.UML.LifeLine), this);
    }


    public override List<global::Data.MOF.MofElement> BuildMofFromJson(BsonDocument json, XmiCollection container)
    {
        List<global::Data.MOF.MofElement> result = new List<global::Data.MOF.MofElement>();
       
        UML.Interactions.Lifeline mofLifeLine = new UML.Interactions.Lifeline();

        string id = (string)json.GetValue("XmiId");

        mofLifeLine.XmiId = id;

        container.AddMofElement(mofLifeLine);
        result.Add(mofLifeLine);

        return result;
    }

    public override void UpdateMofFromJson(BsonDocument json, XmiCollection container)
    {
        UML.Interactions.Lifeline mofLifeLine = (UML.Interactions.Lifeline)container.GetMofElement((string)json.GetValue("XmiId"));

        mofLifeLine.interaction = (UML.Interactions.Interaction)JsonConvertor.FindMofByXmiId(json, container, "interaction");

        mofLifeLine.ownedComment.Clear();
        foreach (MofElement mof in JsonConvertor.FindMofArrayByXmiId(json, container, "ownedComment"))
            mofLifeLine.ownedComment.Add((UML.CommonStructure.Comment)mof);

        mofLifeLine.ownedElement.Clear();
        foreach (MofElement mof in JsonConvertor.FindMofArrayByXmiId(json, container, "ownedElement"))
            mofLifeLine.ownedElement.Add((UML.CommonStructure.Element)mof);

        mofLifeLine.coveredBy.Clear();
        foreach (MofElement mof in JsonConvertor.FindMofArrayByXmiId(json, container, "coveredBy"))
            mofLifeLine.coveredBy.Add((UML.Interactions.InteractionFragment)mof);

        mofLifeLine.name = json.GetValue("name").ToString();

        mofLifeLine.decomposedAs = (UML.Interactions.PartDecomposition)JsonConvertor.FindMofByXmiId(json, container, "decomposedAs");

        mofLifeLine.owner = (UML.CommonStructure.Element)JsonConvertor.FindMofByXmiId(json, container, "owner");
    }
    
    public override List<MofElement> BuildMofFromDg(DgElement dg, XmiCollection container)
    {
        List<global::Data.MOF.MofElement> result = new List<global::Data.MOF.MofElement>();
        
        UML.Interactions.Lifeline MofLifeLine = new UML.Interactions.Lifeline();
        MofLifeLine.generateNewXmiId();

        MofLifeLine.dgElement.Add(dg);
        dg.mofElement.Add(MofLifeLine);

        container.AddMofElement(MofLifeLine);
        result.Add(MofLifeLine);

        return result;
    }

    public override void UpdateMofFromDg(DgElement dg, XmiCollection container)
    {
        UML.Interactions.Lifeline mofLifeline = (UML.Interactions.Lifeline)dg.mofElement[0];
        global::DG.UML.LifeLine dgLifeline = (global::DG.UML.LifeLine)dg;
        global::DG.UML.SequenceDiagram dgDiagram = dgLifeline.GetSequenceDiagram();
        UML.Interactions.Interaction mofDiagram = (UML.Interactions.Interaction)dgDiagram.mofElement[0];

        // coveredBy 
        mofLifeline.coveredBy.Clear();
        mofLifeline.coveredBy.Add(mofDiagram);
        foreach(global::DG.UML.Message dgMessage in dgDiagram.GetMessagesRec())
        {
            UML.Interactions.Message mofMessage = (UML.Interactions.Message) dgMessage.mofElement[0];
            mofLifeline.coveredBy.Add(mofMessage.sendEvent);
            mofLifeline.coveredBy.Add(mofMessage.receiveEvent);
        }
        foreach (global::DG.UML.Fragment dgFragment in dgDiagram.GetFragmentsRec())
        {
            UML.Interactions.CombinedFragment mofFragment = (UML.Interactions.CombinedFragment)dgFragment.mofElement[0];
            mofLifeline.coveredBy.Add(mofFragment);
        }

        // name
        mofLifeline.name = dgLifeline.GetName();

        // interaction
        mofLifeline.interaction = mofDiagram;

        // owner 
        mofLifeline.owner = mofDiagram;
        
        //ownedElement
        mofLifeline.ownedElement.Clear();

        //ownedComment 
        mofLifeline.ownedComment.Clear();

        // represents
        /* TODO     do not implement yet    */

        // selector
        /* TODO     do not implement yet    */

        // decomposedAs
        /* TODO     do not implement yet    */
        
    }

    public override List<DgElement> BuilDgFromMof(MofElement mof, XmiCollection container)
    {
        // Build
        List<global::DG.DgElement> result = new List<global::DG.DgElement>();
        var mofLifeline =  (UML.Interactions.Lifeline)mof;

        GameObject go = GameObject.Instantiate(PrefabLifeline);
        global::DG.UML.LifeLine dg = go.GetComponent<global::DG.UML.LifeLine>();
        result.Add(dg);

        // Map references

        dg.mofElement.Add(mofLifeline);
        mofLifeline.dgElement.Add(dg);

        return result;
    }
    public override void UpdateDgFromMof(DgElement dg, XmiCollection container)
    {
        UML.Interactions.Lifeline mofLifeline = (UML.Interactions.Lifeline)dg.mofElement[0];
        LifeLine dgLifeline = (LifeLine)dg;

        dgLifeline.SetName(mofLifeline.name);
    }




}

