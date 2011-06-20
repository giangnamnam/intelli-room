﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using System.Reflection;

namespace IntelliRoom
{
    public class Events
    {

        List<Action> actions;

        public Events()
        {
            IntelliRoomSystem.camera.finishImageProcess += new Action<Camera.LastResults>(camera_finishImageProcess);
            IntelliRoomSystem.camera.iluminanceEvent += new Action<double>(camera_iluminanceEvent);
            IntelliRoomSystem.camera.movementDetected += new Action<double>(camera_movementDetected);
            IntelliRoomSystem.camera.peopleDetected += new Action<Camera.FaceResult>(camera_peopleDetected);
            InfoMessages.newMessage += new Action<InfoMessages.Message>(InfoMessages_newMessage);
            IntelliRoomSystem.voiceEngine.speechRecognizer += new EventHandler<System.Speech.Recognition.RecognitionEventArgs>(voiceEngine_speechRecognizer);
            actions = new List<Action>();
        }

        void voiceEngine_speechRecognizer(object sender, System.Speech.Recognition.RecognitionEventArgs e)
        {
            CheckEvent("speechRecognizer");
        }

        void InfoMessages_newMessage(InfoMessages.Message obj)
        {
            CheckEvent("newMessage");
        }

        void camera_peopleDetected(Camera.FaceResult obj)
        {
            CheckEvent("peopleDetected");
        }

        void camera_movementDetected(double obj)
        {
            CheckEvent("movementDetected");
        }

        void camera_iluminanceEvent(double obj)
        {
            CheckEvent("iluminanceEvent");
        }

        void camera_finishImageProcess(Camera.LastResults obj)
        {
            CheckEvent("finishImageProcess");
        }

        public void AddAction (string nameEvent, string command)
        {
            actions.Add(new Action(nameEvent,command));
        }


        private void CheckEvent(string nameEvent)
        {
            List<Action> execute = actions.Where(x => x.EventName.ToLower() == nameEvent.ToLower()).ToList<Action>();

            foreach (Action act in execute)
            {
                act.ExecuteAction();
            }
        }
    }

    public class Action
    {
        private string eventName;
        private string command;

        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }

        public string Command
        {
            get { return command; }
            set { command = value; }
        }

        public Action(string eventName, string action)
        {
            this.eventName = eventName;
            this.command = action;
        }

        public void ExecuteAction()
        {
            string[] commands = command.Split(new char[] { '|' });
            foreach (string cmd in commands)
            {
                 Execute(cmd);
            }
        }

        private void Execute(string command)
        {
            String[] separateCommand = SeparateArguments(command);
            MethodInfo[] methods = Reflection.SearchSpeakMethod(separateCommand[0]);
            String result = "";
            //sacamos los parametros
            string[] parametres = new string[separateCommand.Length - 1];

            for (int i = 1; i < separateCommand.Length; i++)
            {
                parametres[i - 1] = separateCommand[i];
            }

            if (methods != null)
            {
                //hay al menos un metodo con ese nombre
                foreach (MethodInfo mi in methods)
                {
                    if (mi.GetParameters().Length == separateCommand.Length - 1)
                    {
                        //hay un metodo con el mismo numero de parametros
                        object resultObj = Reflection.Invoke(mi, parametres);
                        if (resultObj != null)
                            result = resultObj.ToString();
                        break; //para no ejecutar mas de uno
                    }
                }
            }
            //return result;
        }

        private String[] SeparateArguments(String command)
        {
            String[] result = command.Split(new char[] { ' ' });
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Replace("_", " ");
            }
            return result;
        }
    }

}