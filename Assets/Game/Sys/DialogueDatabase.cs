using UnityEngine;
using System.Collections;

public class DialogueDatabase : MonoBehaviour {
	
	public DialogueData 
		TheBattleBegins,
		AtTheGateOfPlagueKing,
		TheTideTurns,
		ThePlague,
		AtTheGateOfReverendKing,
		MeetingThePlagueKing,
		MeetingTheReverendKing,
		ZOMBIE_MeetingThePlagueKing,
		ZOMBIE_MeetingTheReverendKing,
		SlayingThePlagueKing,
		SlayingTheReverendKing,
		SoldierAllyRandomStart,
		SoldierAllyRandom1,
		SoldierAllyRandom2,
		
		EndDialogueData,
		EndDialogueEndConversation,
		EndDialogueNoComment,
		EndDialogueAwkwardSilence;
	
	// Use this for initialization
	void Start () {
		
		DialogueData ans1,ans2,ans3,ka1,ka2,ka3;

		//end messages
		
		EndDialogueEndConversation=new DialogueData("<End conversation>","ENDL");
		EndDialogueNoComment=new DialogueData("<No comment>","ENDL");
		EndDialogueAwkwardSilence=new DialogueData("<AwkwardSilence>","ENDL");
		
		EndDialogueData=new DialogueData("");
		EndDialogueData.AddAnswer(EndDialogueEndConversation);
		
		
		//start dialogue 1
		TheBattleBegins=new DialogueData("The time has come!\n\nSlay the vile Plague King and his army.\nThis is thy destiny!");
		
		ans1=new DialogueData("I will bring you the top most part of this false King.");
		/*
		ans2=new DialogueData("Checkers will be pointy with the Chips of the Plague Pawns.");
		ans3=new DialogueData("We shall force the Plague King to surrender.");
		*/
		
		ans2=new DialogueData("Can we postpone this?");

		ans3=new DialogueData("I.. I..");	

		TheBattleBegins.AddAnswer(ans1);
		TheBattleBegins.AddAnswer(ans2);
		TheBattleBegins.AddAnswer(ans3);
		
		ka1=new DialogueData("Splendid!");
		ans1.AddAnswer(ka1);
		
		/*ka1=new DialogueData("The day will be ours. Charge!"); random
		ans1.AddAnswer(ka1);
		ka1=new DialogueData("Let strength be grant, so the world might be mended.");
		ans2.AddAnswer(ka1);
		ka1=new DialogueData("It matters not, His Pestilence shall warm my house as a log.");
		ans3.AddAnswer(ka1);*/
		
		
		ka1=new DialogueData("Absolutely not!\n\nHave at them!");
		ans2.AddAnswer(ka1);
		ka1=new DialogueData("What is it!\nSpit it out!!");
		ans3.AddAnswer(ka1);
		

		
		ka1.AddAnswer(new DialogueData("Nothing...","ENDL"));
		ans1=new DialogueData("I cannot do what you ask of me.");
		ka1.AddAnswer(ans1);
		
		ka1=new DialogueData("What is this! Do you dare to defy your king!");
		ans1.AddAnswer(ka1);
		
		ans1=new DialogueData("Yes. That is it. Precisely,");
		ka1.AddAnswer(ans1);
		ans2=new DialogueData("I changed my mind. Forgive me for questioning your royal reasons.");
		ka1.AddAnswer(ans2);
		
		ka1=new DialogueData("I have no need for traitorous heretics!\n\nBegone foul shade!");
		ans1.AddAnswer(ka1);
		
		ans1=new DialogueData("So be it.","GAMEOVER_GAVEUP");
		ka1.AddAnswer(ans1);
		
		ka2=new DialogueData("Good, now make up for this unfortunate slip by claiming the land.");
		ans2.AddAnswer(ka2);
		
		

		/*Plague King voi kommentoida seuraavilla:
		PK: Go forth and let them bleed!
		PK: Bring me His brains!
		PK: Enough of this chip chat, chap. Show your worth!
		*/

		// The Tide Turns
		
		TheTideTurns=new DialogueData("We are smiled upon. The enemy flees.\n\n Cut them down!\nHave no mercy!");
		
		ans1=new DialogueData("Eat them up, boys!", "ENDL");
		ans2=new DialogueData("As you command sire.", "ENDL");
		TheTideTurns.AddAnswer (ans1);
		TheTideTurns.AddAnswer (ans2);
		TheTideTurns.AddAnswer (EndDialogueNoComment);
		/*
		Vaihtoehtoinen dialogi
		 
		RK: Cut down that rook!

		PK:  I will make those who stay the envy of those who return.
		*/
		
		//The Plague
		
		//Reverend King sanoo
		ThePlague=new DialogueData("What madness is this?");
		
		//Plague King vastaa
		ans1=new DialogueData("The plague. It's spreading!\nWe must be cautious", "ENDL");
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
		ans2=new DialogueData("Your viral campaign is finished Mr. Plague King.");
		ans3=new DialogueData("*lie* This is public health commission. There have been complaints about spore clouds. Please let us in.");
		
		ka1=new DialogueData("Who’s there?");
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
		
/*
		//temp
		King1InTower=new DialogueData("What is this!! You smahed my gate!");
		
		ans1=new DialogueData("I will go now","LEAVE_PLAYERTOWER");
		King1InTower.AddAnswer(ans1);
		
		EnemiesPlague=new DialogueData("What kind of dark magic is this?");
		
		EnemiesFlee=new DialogueData("They flee cut them down");
		 */
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
		
		MeetingThePlagueKing=new DialogueData("Well, well. Isn't it my adversary's errant boy");
		
		ans1=new DialogueData("Plague King, meet sword.");
		ans2=new DialogueData("Surrender and you shall be mostly spared.\nWe might have to sand some of your worst edges off, though.");
		ans3=new DialogueData("This plague of yours is a powerful tool.\nWe could take over the world together.");
		
		ka1=new DialogueData("I should have eaten more sausages when I had the chance.");
		ka2=new DialogueData("All I ever wanted was someone to talk to.\nSo sad/confused atm.");
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
		
		ans1=new DialogueData("*Malicious neighing*","GAMEOVER_EVILALLIANCE");
		ka3.AddAnswer (ans1);
		
		ans1=new DialogueData("You can ruminate more on that in the dungeons.", "GAMEOVER_PLAGUEKINGSURRENDER");
		ka2.AddAnswer(ans1);
		
		ans1=new DialogueData("*Stab*","GAMEOVER_PLAGUEKINGDEAD");
		ka1.AddAnswer(ans1);
		
		//Meeting the Reverend King
		MeetingTheReverendKing=new DialogueData("That gate cost more than my crown. What do you think you're doing?");
			
		ans1=new DialogueData("Nice view, boss.");
		ans2=new DialogueData("I don’t feel like this job is for me. I dream of green pastures and galloping.");
		ans3=new DialogueData("The King is dead, long live the King!");
		
		ka1=new DialogueData("It is... But enough of this horseplay!\n Go back and do my bidding.");
		ka2=new DialogueData("Surely you have the strength to carry through.\n\nAfter the Plague King has been snuffed out all of us will be free.");
		ka3=new DialogueData("That glimmer in your eyes seems conclusive.\n\nBy slaying me, you are only exchanging obligations to slavery.");
		
		MeetingTheReverendKing.AddAnswer (ans1);
		MeetingTheReverendKing.AddAnswer (ans2);
		MeetingTheReverendKing.AddAnswer (ans3);
		
		ans1.AddAnswer (ka1);
		ans2.AddAnswer (ka2);
		ans3.AddAnswer (ka3);
		
		//ans1=new DialogueData("It was... Enough of this horseplay. Now go back and do my bidding.");
		//ka1.AddAnswer (ans1);
		
		ans1=new DialogueData("Sure thing boss. Have a nice day.", "LEAVE_PLAYERTOWER");
		ka1.AddAnswer (ans1);
		var no_appreciation=new DialogueData("I think my work is not appreciated. Actually, I’d make a much better king. Maybe violence IS a solution? Mr. Pointy hat, have a taste of my blade. ");
		ka1.AddAnswer (no_appreciation);
		
		ka1=new DialogueData("From where the sun now stands I will fight no more forever.");
		no_appreciation.AddAnswer (ka1);
		
		ans1=new DialogueData("You're right. But don't call me shirley.","LEAVE_PLAYERTOWER");
		ka2.AddAnswer(ans1);
		ka2.AddAnswer(no_appreciation);
		
		ans1=new DialogueData("So you say.");
		ka3.AddAnswer(ans1);
		
		ans1.AddAnswer(ka1);
		
		ans1=new DialogueData("*Stab*","GAMEOVER_KINGDEAD");
		ka1.AddAnswer(ans1);
		
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
		
		ZOMBIE_MeetingTheReverendKing=new DialogueData("That gate cost more than...\nUh oh.\n\nWhat has happened to you!");
		
		ans1=new DialogueData("Brains...");
		ZOMBIE_MeetingTheReverendKing.AddAnswer(ans1);
		
		
		ka1=new DialogueData("Oh no.. no no No!\n\nAAAAAAArrGGghhh!");
		ans1.AddAnswer(ka1);
		
		ans1=new DialogueData("Tastes.. like.. chicken..","GAMEOVER_ZOMBIEKING");
		ka1.AddAnswer(ans1);
		
		ZOMBIE_MeetingThePlagueKing=new DialogueData("I see you have taken the plague to your heart.\n\nHow nice of you.");
		
		ans1=new DialogueData("Brains...!");
		ans3=new DialogueData("Urgh...");
		ZOMBIE_MeetingThePlagueKing.AddAnswer(ans1);
		ZOMBIE_MeetingThePlagueKing.AddAnswer(ans3);
		
		ka1=new DialogueData("What? How dare you\n\nI command you to stop.");
		ans1.AddAnswer(ka1);
		
		ans1=new DialogueData("<Stop advancing>");
		ans2=new DialogueData("<Continue advancing>");
		ka1.AddAnswer(ans1);
		ka1.AddAnswer(ans2);
		
		ka1=new DialogueData("Good.. This one is obidient.\n\nNow be a good knight and slay your former master.");
		ans1.AddAnswer(ka1);
		ans3.AddAnswer(ka1);
		
		ans1=new DialogueData("...","LEAVE_ENEMYTOWER");
		ka1.AddAnswer(ans1);
		
		ka1=new DialogueData("Oh NOES!!! The irony!");
		ans2.AddAnswer(ka1);
		
		ans1=new DialogueData("Yummy...","GAMEOVER_ZOMBIEPLAGUEKING");
		ka1.AddAnswer(ans1);
		
		//soldier random
		SoldierAllyRandom1=new DialogueData("Must continue fighting!");
		
		ans1=new DialogueData("Carry on.","ENDL");
		ans2=new DialogueData("You do that.","ENDL");
		
		SoldierAllyRandom1.AddAnswer(ans1);
		SoldierAllyRandom1.AddAnswer(ans2);
		
		SoldierAllyRandom2=new DialogueData("EXTERMINATE!");
		
		
		
		SoldierAllyRandomStart=new DialogueData("","RANDOM");
		
		SoldierAllyRandomStart.AddAnswer(SoldierAllyRandom1);
		SoldierAllyRandomStart.AddAnswer(SoldierAllyRandom2);
	}
}
