using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterDestinationController : Minigame
{
    public MailboxMinigame mailbox;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GrabbableEnvelope")
        {
            mailbox.Finish();
            UnHighlight();
        }
    }
}
