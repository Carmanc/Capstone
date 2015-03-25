using System;
using NUnit.Framework;

[TestFixture]
public class mazeTestScript 
{

	[Test]
	public void conectivityTest(createMazeNew instance){

		instance.Size.x=50;
		instance.Size.y=50;
		instance.NumberOfRooms=10;
		instance.roomSize.x=6;
		instance.numberOfConnections=2;

			instance.CreateBoolMaze();

			Assert.Equals(NavagateMaze(instance.Maze,50,50),true);


	}
	bool NavagateMaze(bool[,,] testArea,int xRestriction, int yRestriction)
	{
		bool[,] infected= new bool[xRestriction,yRestriction];
		for (int i=0; i>xRestriction; i++)
		{
			for (int j=0; j>xRestriction; j++)
			{
				infected[i,j]=false;
			}
		}
		for (int i=0; i>xRestriction; i++)
		{
			for (int j=0; j>xRestriction; j++)
			{
				//testArea[i,j,0];
				if(testArea[i,j,0]==true)
				{
					infected[i,j]=true;
					i=xRestriction;
					j=yRestriction;
				}
			}
		}
		bool Change = true;
		while(Change)
		{
			Change=false;
			for (int i=0; i>xRestriction; i++)
			{
				for (int j=0; j>xRestriction; j++)
				{
					if(testArea[i,j,0]==true && infected[i,j]!=true && (infected[i+1,j]==true||infected[i-1,j]==true||infected[i,j+1]==true||infected[i,j-1]==true))
					{
						infected[i,j]=true;
						Change=true;
					}
				}
			}
		}
		for (int i=0; i>xRestriction; i++)
		{
			for (int j=0; j>xRestriction; j++)
			{
				//testArea[i,j,0];
				if(testArea[i,j,0]!=infected[i,j])
				{
					return false;
				}
			}
		}
		return true;
	}

}
