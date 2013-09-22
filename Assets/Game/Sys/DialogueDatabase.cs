﻿using UnityEngine;
using System.Collections;

public class DialogueDatabase : MonoBehaviour {
	
	public DialogueData TheBattleBegins,EndDialogueData, AtTheGateOfPlagueKing, TheTideTurns, ThePlague, AtTheGateOfReverendKing, MeetingThePlagueKing, MeetingTheReverendKing, SlayingThePlagueKing, SlayingTheReverendKing;
	
	// Use this for initialization
	void Start () {
		
		DialogueData ans1,ans2,ans3,ans4,ka1,ka2,ka3;
		
		//start dialogue 1
		TheBattleBegins=new DialogueData("The time has come!\n\nSlay this vile king and his army.");
		
		ans1=new DialogueData("I will bring you the top most part of this false King.");
		ans2=new DialogueData("Checkers will be pointy with the Chips of the Plague Pawns.");
		ans3=new DialogueData("We shall force the Plague King to surrender.");
		
		TheBattleBegins.AddAnswer(ans1);
		TheBattleBegins.AddAnswer(ans2);
		TheBattleBegins.AddAnswer(ans3);
		
		ka1=new DialogueData("The day will be ours. Charge!");
		ans1.AddAnswer(ka1);
		ka1=new DialogueData("Let strength be grant, so the world might be mended.");
		ans2.AddAnswer(ka1);
		ka1=new DialogueData("It matters not, His Pestilence shall warm my house as a log.");
		ans3.AddAnswer(ka1);
		
		/*Plague King voi kommentoida seuraavilla:
		PK: Go forth and let them bleed!
		PK: Bring me His brains!
		PK: Enough of this chip chat, chap. Show your worth!
		*/
		
		//end message
		
		EndDialogueData=new DialogueData("");
		EndDialogueData.AddAnswer(new DialogueData("<End conversation>","ENDL"));
		
		// The Tide Turns
		
		TheTideTurns=new DialogueData("We are smiled upon. Cut them down!");
		
		ans1=new DialogueData("Eat them up, boys!", "ENDL");
		TheTideTurns.AddAnswer (ans1);
		/*
		Vaihtoehtoinen dialogi
		 
		RK: Cut down that rook!

		PK:  I will make those who stay the envy of those who return.
		*/
		
		//The Plague
		
		//Reverend King sanoo
		ThePlague=new DialogueData("What madness is this?");
		
		//Plague King vastaa
		ans1=new DialogueData("You shall shout Europe at the sink and you shall enjoy it!", "ENDL");
		ThePlague.AddAnswer (ans1);
			
		/* Vaihtoehtoinen dialogi
		
		RK: What madness is this?
		RK: That’s just… sick.
		RK: Their changing colour now? That’s confusing…

		PK: We are the plague to end all plagues.

		*/
			
		// At the Gate of the Plague King
		
		AtTheGateOfPlagueKing=new DialogueData("You stand at the gate of the Plague King.");
		
		ans1=new DialogueData("Knock, knock.");
		ans2=new DialogueData("Your viral campaign is finished Plague King.");
		ans3=new DialogueData("*lie* This is public health commission. There have been complaints about spore clouds. Please let us in.");
		
		ka1=new DialogueData ("Who’s there?");
		ka2=new DialogueData("I think you have a wrong address. Please try next door.");
		ka3=new DialogueData("Come on up and we’ll make some fungi sausage together.");
		
		//linkitetään dialogi
		
		AtTheGateOfPlagueKing.AddAnswer (ans1);
		AtTheGateOfPlagueKing.AddAnswer (ans2);
		AtTheGateOfPlagueKing.AddAnswer (ans3);
		
		ans1.AddAnswer (ka1);
		ans2.AddAnswer (ka3);
		ans3.AddAnswer (ka2);
		
		//seuraavat datat + linkit
		
		//pelaajan vastaukset ja linkit edelliseen kuninkaan vastaukseen
		ans1=new DialogueData("Meat.");
		ka1.AddAnswer (ans1);
		
		
		//kuninkaan vastaukset ja linkit edellisiin pelaajan vastauksiin
		ka1=new DialogueData ("Meat who?");
		ans1.AddAnswer (ka1);
		
		
		//seuraavat datat + linkit
		
		ans1=new DialogueData ("Meet your doom!", "ENDL");
		ka1.AddAnswer (ans1);
		
		// At the Gate of the Reverend King
		
		AtTheGateOfReverendKing=new DialogueData("You stand at the gate of the Reverend King, your liege.");
		
		ans1=new DialogueData("I shall stand by your pointiness, just let me in!");
		ans2=new DialogueData("I forgot my sword inside, please let me in.");
		ans3=new DialogueData("I don’t think we’re going to win. There’s so many of them and I’m scared like a little pony.");
		//ans4 vain kun pelaajalla on tauti ("Let’s hug. Let’s kiss. Let’s eat together.")
		//Reverend Kingin vastaus tähän on ("All that foam in your mouth took my appetite away.")
		//Plague King kommentoi tähän ("Mmm… Candlelight nemesis consumerism.")
		
		ka1=new DialogueData("Where I point, you shall fight. Stay there!", "ENDL");
		ka3=new DialogueData("There’s no staircase. Hop off!", "ENDL");
		ka2=new DialogueData("Hide somewhere else, this spot is taken!", "ENDL");
		
		AtTheGateOfReverendKing.AddAnswer (ans1);
		AtTheGateOfReverendKing.AddAnswer (ans2);
		AtTheGateOfReverendKing.AddAnswer (ans3);
		
		ans1.AddAnswer (ka1);
		ans2.AddAnswer (ka3);
		ans3.AddAnswer (ka2);
		
		// Meeting the PlagueKing
		
		MeetingThePlagueKing=new DialogueData("You stand before the Plague King.");
		
		ans1=new DialogueData("Plague King, meet sword.");
		ans2=new DialogueData("Surrender and you shall be mostly spared. Although we might have to belt sand some of your worst edge off, though.");
		ans3=new DialogueData("This plague of yours is a powerful tool. We could take over the world together.");
		
		ka1=new DialogueData("I should have eaten more sausage when I had the chance.", "ENDL");
		ka2=new DialogueData("All I ever wanted was someone to talk to. So sad/confused atm.", "ENDL");
		ka3=new DialogueData("I will give you 49% of company stocks. Let’s bond orally.");
		
		MeetingThePlagueKing.AddAnswer (ans1);
		MeetingThePlagueKing.AddAnswer (ans2);
		MeetingThePlagueKing.AddAnswer (ans3);
		
		ans1.AddAnswer (ka1);
		ans2.AddAnswer (ka2);
		ans3.AddAnswer (ka3);
				
		ans3=new DialogueData("*Evil neighing*");
		ka3.AddAnswer (ans3);
		
		ka3=new DialogueData ("*Evil laughter*", "ENDL");
		ans3.AddAnswer (ka3);
		
		//Meeting the Reverend King
		
		MeetingTheReverendKing=new DialogueData("That gate cost more than my crown. What do you think you're doing?");
			
		ans1=new DialogueData("Nice view, boss.");
		ans2=new DialogueData("I don’t feel like this job is for me. I dream of green pastures and galloping.");
		ans3=new DialogueData("The King is dead, long live the King.");
		
		ka1=new DialogueData("It was... Enough of this horseplay. Now go back and do my bidding.");
		ka2=new DialogueData("Surely you have the strength to carry through. After the Plague King has been snuffed out all us will be free.", "ENDL");
		ka3=new DialogueData("That glimmer in your eyes seems conclusive. By slaying me, you are only exchanging obligations to slavery.");
		
		MeetingTheReverendKing.AddAnswer (ans1);
		MeetingTheReverendKing.AddAnswer (ans2);
		MeetingTheReverendKing.AddAnswer (ans3);
		
		ans1.AddAnswer (ka1);
		ans2.AddAnswer (ka2);
		ans3.AddAnswer (ka3);
		
		//ans1=new DialogueData("It was... Enough of this horseplay. Now go back and do my bidding.");
		//ka1.AddAnswer (ans1);
		
		ans1=new DialogueData("Sure thing boss. Have a nice day.", "ENDL");
		ka1.AddAnswer (ans1);
		var no_appreciation=new DialogueData("I think my work is not appreciated. Actually, I’d make a much better king. Maybe violence IS a solution? Mr. Pointy hat, have a taste of my blade. ");
		ka1.AddAnswer (no_appreciation);
		
		ka1=new DialogueData("From where the sun now stands I will fight no more forever.");
		no_appreciation.AddAnswer (ka1);
		
		ans1=new DialogueData("You're right. But don't call me shirley.","ENDL");
		ka2.AddAnswer(ans1);
		ka2.AddAnswer(no_appreciation);
		
		//Slaying the Plague King
		
		SlayingThePlagueKing=new DialogueData("I only wanted everyone to get along... to belong... to me");
		
		/* Vaihtoehtoinen dialogi
		 	HK: Pop goes the false king.
			RK: A miraculous deliverance. Mead and meat are truly deserved... just remember to wash your hands.

			RK: I shall rule forever.
			HK: We shall rule together.

			As an infected:
			HK: There’s enough room for both us where I’m going.
			PK: Not off the board, please no!
		 */
		
		//Slaying the Reverend King
		
		SlayingTheReverendKing=new DialogueData("King for a day, fool for a lifetime.");
		
		/*	Jos pelaajalla on tartunta
		 	
			HK: Tastes like chicken.
			PK: Plague. Crowd control since 1347.

		 */
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
