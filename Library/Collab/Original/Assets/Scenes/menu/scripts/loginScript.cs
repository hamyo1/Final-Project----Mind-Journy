﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;

public class loginScript : MonoBehaviour {

    public TMP_InputField UserName;
    public TMP_InputField Password;
    //private usersDB user = GetComponent<usersDB>();
    static void Connect (String server, String message) {
        try {
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer
            // connected to the same address as specified by the server, port
            // combination.
            Int32 port = 4999;
            TcpClient client = new TcpClient (server, port);

            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes (message);

            // Get a client stream for reading and writing.
            //Stream stream = client.GetStream();

            NetworkStream stream = client.GetStream ();

            // Send the message to the connected TcpServer.
            stream.Write (data, 0, data.Length);

            Debug.Log ("Sent: ");

            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read (data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString (data, 0, bytes);
            Debug.Log ("Received: " + responseData);
            string input = String.Empty;
           try {
               int result = Int32.Parse (responseData);
                //Console.WriteLine (result);
                if (result == 1) Debug.Log ("connected");
                else Debug.Log ("disconnected");
            } catch (FormatException) {
                Debug.Log("Unable to parse '{}'");
           }

            // Close everything.
            stream.Close ();
            client.Close ();
        } catch (ArgumentNullException e) {
            Debug.Log ("ArgumentNullException: ");
        } catch (SocketException e) {
            Debug.Log ("SocketException: ");
        }

        Debug.Log ("\n Press Enter to continue...");
    }

    public void submitInput () {

        //Debug.Log ("start connection to server and db");
        Connect ("localhost", "{\"action\":\"connect\",\"name\":\"" + UserName.text + "\",\"pass\":\"" + Password.text + "\"}\n");
        Debug.Log ("start connection to server and db");
        Debug.Log ("{\"action\":\"register\",\"name\":\"" + UserName.text + "\",\"pass\":\"" + Password.text + "\"}\n");
        //Debug.Log ("{\"action\":\"register\",\"name\":\""+UserName.text);

    }

}