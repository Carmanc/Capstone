using UnityEngine;
using System.Collections;

public class createMazeNew : MonoBehaviour {
	
	public int levelNumber;
	public Transform Floor;
	public Transform Player;
	//public GameObject Player;
	public Transform End;
	public Transform WallUnit;
	public Transform doorUnit;
	public Transform doorUnitUp;
	//public Transform Room3by3;
	public Transform[] room3By3Op1 = new Transform[3];// 3 By 3 Versions With up to two pills in a room
	public Transform[] room3By3Op2 = new Transform[3];//Two different raiants on this room due to the fact it is the least likely to be generated
	/*public Transform Room4by4ZeroPill;
	public Transform Room4by4OnePill;
	public Transform Room4by4TwoPill;*/
	public Transform[] room4By4Op1 = new Transform[3];//Replaced the room with array of same room differnt pill numbers
	public Transform[] room4By4Op2 = new Transform[3];
	public Transform[] room4By4Op3 = new Transform[3];
	public Transform[] room4By4Op4 = new Transform[3];
	public Transform[] room4By4Op5 = new Transform[3];//Five different variants of the 4x4 room tyoe because it is the most common type of room to be generated
	public Transform[] room5By5Op1 = new Transform[3];//5 By 5 Versions With up to two pills in a room
	public Transform[] room5By5Op2 = new Transform[3];
	public Transform[] room5By5Op3 = new Transform[3];// Three for this type because it is the second most likely to be generated
	public Transform StartRoom3x3;
	public Transform StartRoom4x4;
	public Transform StartRoom5x5;
	//public Transform Room5by5;
	public Transform SingleSideUnit;//hall decoration. May noy implement this
	//public Transform RoofUnit;
	//public Transform LightRoofUnit;
	public int NumberOfEnemies;
	public Vector2 Size;
	public int NumberOfRooms;//Can't Exceed 100 Will make that impossible by moding this number by 100 on startup;
	public Transform Enemy1;
	public Transform Enemy2;
	public Transform Enemy3;
	public Vector2 roomSize;//Used because we may want to implement a variable x and y length 
	public int numberOfConnections;
	public int numPillsToAddToMaze;
	
	private Vector2 StartPoint;
	private Vector2 mazeEndPoint;
	
	private bool[,,] Maze;
	private Vector3[] roomNodes;//x is x cordinate, y is y cordinate, z is size
	private bool[,] Connection;
	private Object usedPlayer;
	private int[] numPillsInRoom = new int[100]; 
	
	// Use this for initialization
	void Start () {
		NumberOfRooms=NumberOfRooms%100;
		if(NumberOfRooms == 0|| NumberOfRooms == 1)
		{
			NumberOfRooms=2;
		}
		setUpPills();
		CreateBoolMaze();
		CreateGrid();
		SettingsSingleton.Instance.applySound();
	}
	
	void CreateBoolMaze()
	{
		setUpMazeBorder();
		Connection = new bool [NumberOfRooms,NumberOfRooms];
		
		//Set up Connection Matrix
		for(int i = 0; i < NumberOfRooms; i++)
		{
			for(int j = 0; j < NumberOfRooms; j++)
			{
				Connection[i,j]=false;
			}
		}
		
		roomNodes = new Vector3[NumberOfRooms];
		//Random.seed=42;
		int Rand1;
		int Rand2;
		int Rand3;
		bool goodFlag=true;
		
		for(int k = 0; k < NumberOfRooms; k++)
		{
			goodFlag=true;
			Rand1= (int)((Random.value * Size.x)%Size.x);
			Rand2= (int) ((Random.value * Size.y)%Size.y);
			
			if(Maze[Rand1,Rand2,1]==true)
			{
				k--;
			}
			else
			{
				Rand3= (int)(4+(Random.value*(roomSize.x-4)));
				
				for(int l = -2; l < (Rand3+3); l++)
				{
					for(int m = -2; m < (Rand3+3); m++)
					{
						if(((Rand1+l)>=0)&&((Rand2+m)>=0)&&((Rand2+m)<Size.y)&&((Rand1+l)<Size.x))
						{
							if(Maze[(Rand1+l),(Rand2+m),1]==true)
							{
								k--;// Popping out of the loops
								m=Rand3+3;
								l=Rand3+3;
								goodFlag=false;
							}
						}
					}
				}
				if(goodFlag==true)
				{
					roomNodes[k].x=Rand1;//X Position
					roomNodes[k].y=Rand2;//Y Position
					roomNodes[k].z=Rand3;//Room Size
					for(int l = 0; l < (Rand3+1); l++)
					{
						for(int m = 0; m < (Rand3+1); m++)
						{
							if(l == (Rand3) || m == (Rand3) || l==0 || m ==0)
							{
								if(l==2 || m==2)
								{
									Maze[(Rand1+l),(Rand2+m),1]=false;
								}
								else
								{
									Maze[(Rand1+l),(Rand2+m),1]=true;
								}
							}
							else
							{
								Maze[(Rand1+l),(Rand2+m),0]=true;
								Maze[(Rand1+l),(Rand2+m),1]=true;
							}
						}
					}
				}
			}
		}
		//For Determining Connections
		for(int i = 0; i < numberOfConnections; i++)//Set Up The connections between Rooms total of numberOfConnections
		{
			if(i==0)
			{
				for(int j = 0; j < (NumberOfRooms-1); j++)
				{
					
					Connection[j,(j+1)]=true;
					
				}
				
			}
			else
			{
				for(int j = 0; j < (NumberOfRooms/2); j++)//TODO: Fix connections made here
				{
					Rand1 = (int)((NumberOfRooms/2)+Random.value * (NumberOfRooms/2));
					if(Connection[j,Rand1]==true)
					{
						j--;
					}
					else
					{
						Connection[Rand1,j] = true;
						//Connection[j,Rand1] = true;
					}
				}
			}		
		}	
		setUpHalls();
	}
	void setPlayer()
	{
		int temp=((int)roomNodes[0].z)/2;
		StartPoint.x = roomNodes[0].x + temp;
		StartPoint.y = roomNodes[0].y + temp;
		
	}
	void setEndPoint()
	{
		int temp=((int)roomNodes[NumberOfRooms-1].z)/2;
		mazeEndPoint.x = roomNodes[NumberOfRooms-1].x + temp;
		mazeEndPoint.y = roomNodes[NumberOfRooms-1].y + temp;
	}
	void setUpHalls()
	{
		Vector4 Temp;
		Vector2 temp1;
		Vector2 temp2;
		
		for(int i =0; i<NumberOfRooms; i++)
		{
			for(int j = i; j < NumberOfRooms;j++)
			{
				if(Connection[i,j] == true)
				{
					Temp= FindDoors(roomNodes[i],roomNodes[j]);
					//Debug.Log(Temp);
					temp1 = new Vector2(Temp.x,Temp.y);
					temp2 = new Vector2(Temp.z,Temp.w);
					connectingTheRooms(temp1 , temp2);
				}
			}
		}
	}
	
	void placeDoors()
	{
		for(int i =0; i<NumberOfRooms; i++)
		{
			for(int j=0; j < roomNodes[i].z+1; j++)
			{
				for(int k=0; k < roomNodes[i].z+1;k++)
				{
					if(j==2)
					{
						if(Maze[(int)roomNodes[i].x+j,(int)roomNodes[i].y+k,0]==true && Maze[(int)roomNodes[i].x+j,(int)roomNodes[i].y+k,1]==false)
						{
							Instantiate(doorUnit, new Vector3((roomNodes[i].x + j)*2-1,(float)-0.5,(roomNodes[i].y + k)*2),Quaternion.Euler(0,90,0));
							//Instantiate(doorUnit, new Vector3((roomNodes[i].x + j)*2-1,(float)-0.5,(roomNodes[i].y + k)*2),Quaternion.identity);
						}
					}
					if(k==2)
					{
						if(Maze[(int)roomNodes[i].x+j,(int)roomNodes[i].y+k,0]==true && Maze[(int)roomNodes[i].x+j,(int)roomNodes[i].y+k,1]==false)
						{
							Instantiate(doorUnit, new Vector3((roomNodes[i].x + j)*2,(float)-0.5,(roomNodes[i].y + k)*2),Quaternion.identity);
						} 
					}
				}
			}
		}
	}
	Vector4 FindDoors (Vector3 Node1 , Vector3 Node2)//Will tell where the doors are using x y corrdinates for both rooms
	{
		Vector2[] tempRoom1 = new Vector2 [4];
		Vector2[] tempRoom2 = new Vector2 [4];
		int n1Count=0;
		int n2Count=0;
		for(int i = 0; i < roomSize.x; i++)
		{
			for( int j = 0; j<roomSize.x; j++)
			{
				if(( i==2 && j < Node1.z+1 || j==2 && i < Node1.z+1 ))
				{
					if(Maze[(int)(Node1.x+i),(int)(Node1.y+j),1]==false)
					{
						tempRoom1[n1Count].x=(int)(Node1.x+i);
						tempRoom1[n1Count].y=(int)(Node1.y+j);
						n1Count++;
						
					}
				}
				if(( i==2 && j < Node2.z+1 || j==2 && i < Node2.z+1 ))
				{
					if(Maze[(int)(Node2.x+i),(int)(Node2.y+j),1]==false)
					{
						tempRoom2[n2Count].x=(Node2.x+i);
						tempRoom2[n2Count].y=(Node2.y+j);
						n2Count++;
					}
				}
			}
		}
		/*for(int i=0;i<4;i++)
		{
			Debug.Log(tempRoom1[i]);
			Debug.Log(tempRoom2[i]);
		}*/
		int tempBestDifference = 1000000;
		int temp1Door = 0;
		int temp2Door = 0;
		int tempCheck = 0;
		//Debug.Log("new");
		for(int i =0; i < 4; i++)
		{
			for(int j =0; j < 4; j++)
			{
				tempCheck=(int)(Mathf.Pow((float)(tempRoom1[i].x-tempRoom2[i].x),2)+Mathf.Pow((float)(tempRoom1[j].y-tempRoom2[j].y),2));
				//Debug.Log(tempCheck);	
				if(tempCheck < tempBestDifference)
				{
					tempBestDifference = (int)tempCheck;
					temp1Door = i;
					temp2Door = j;
					
				}
			}
		}
		
		Vector4 Ans;
		
		Ans.x = tempRoom1[temp1Door].x;
		Ans.y = tempRoom1[temp1Door].y;
		Ans.z = tempRoom2[temp2Door].x;
		Ans.w = tempRoom2[temp2Door].y;
		
		return Ans;
	}
	void connectingTheRooms(Vector2 Node1 , Vector2 Node2)
	{
		int q=0;
		Vector2 endPoint = new Vector2 (0,0);
		Vector2 previousPoint= new Vector2 (0,0);
		Vector2 currentPoint= new Vector2 (0,0);
		Vector2 upD=new Vector2 (0,0);
		Vector2 downD=new Vector2 (0,0);
		Vector2 leftD=new Vector2 (0,0);
		Vector2 rightD=new Vector2 (0,0);
		bool[] temp = new bool [4];
		int [] temp1 = new int [4];
		
		Maze[(int)Node2.x , (int)Node2.y, 0]=true;
		//Place hall carving algorithm Here
		if(Maze[(int)(Node2.x+1),(int) Node2.y,1]==false)
		{
			endPoint.x=Node2.x+1;
			endPoint.y=Node2.y;
			Maze[(int)endPoint.x , (int)endPoint.y, 0]=true;
		}
		else if(Maze[(int)(Node2.x-1),(int) Node2.y,1]==false)
		{
			endPoint.x=Node2.x-1;
			endPoint.y=Node2.y;
			Maze[(int)endPoint.x , (int)endPoint.y, 0]=true;
		}
		else if(Maze[(int)Node2.x, ((int)Node2.y+1),1]==false)
		{
			endPoint.x=Node2.x;
			endPoint.y=Node2.y+1;
			Maze[(int)endPoint.x , (int)endPoint.y, 0]=true;
		}
		else if(Maze[(int)Node2.x, (int)(Node2.y-1),1]==false)
		{
			endPoint.x=Node2.x;
			endPoint.y=Node2.y-1;
			Maze[(int)endPoint.x , (int)endPoint.y, 0]=true;
		}
		
		currentPoint=Node1;
		//Debug.Log("Start Point");
		//Debug.Log(currentPoint);
		previousPoint=Node1;
		Maze[(int)currentPoint.x , (int)currentPoint.y, 0]=true;
		//Debug.Log("Start");
		
		do{
			//Debug.Log(currentPoint);
			//Debug.Log(endPoint);
			//Up
			//Debug.Log("Up");
			//Debug.Log(Maze[(int)currentPoint.x  , (int)currentPoint.y+1, 1]);
			if(Maze[(int)currentPoint.x  , (int)currentPoint.y+1, 1]==false)
			{
				temp[0]=Maze[(int)currentPoint.x  , (int)currentPoint.y+2, 1];
				temp[1]=Maze[(int)currentPoint.x-1, (int)currentPoint.y+1, 1];
				temp[2]=Maze[(int)currentPoint.x  , (int)currentPoint.y+1, 1];
				temp[3]=Maze[(int)currentPoint.x+1, (int)currentPoint.y+1, 1];
				upD=new Vector2 ((currentPoint.x),(currentPoint.y+1));
				
				if(temp[2]==true || (temp[0]==true && temp[1]==true && temp[3]==true))
				{
					temp1[0]=5;
				}
				else{
					temp1[0]=3;
				}
				if(previousPoint==upD)//Exception Already Traveled
				{
					temp1[0]=4;
				}
			}
			else{
				temp1[0]=5;
			}
			//Down
			//Debug.Log("Down");
			//Debug.Log(Maze[(int)currentPoint.x  , (int)currentPoint.y-1, 1]);
			if(Maze[(int)currentPoint.x  , (int)currentPoint.y-1, 1]==false)
			{
				temp[0]=Maze[(int)currentPoint.x  , (int)currentPoint.y-2, 1];
				temp[1]=Maze[(int)currentPoint.x-1, (int)currentPoint.y-1, 1];
				temp[2]=Maze[(int)currentPoint.x  , (int)currentPoint.y-1, 1];
				temp[3]=Maze[(int)currentPoint.x+1, (int)currentPoint.y-1, 1];
				downD= new Vector2 ((currentPoint.x),(currentPoint.y-1));
				
				if(temp[2]==true || (temp[0]==true && temp[1]==true && temp[3]==true))
				{
					temp1[1]=5;
				}
				else{
					temp1[1]=3;
				}
				if(previousPoint==downD)//Exception Already Traveled
				{
					temp1[1]=4;
				}
			}
			else
			{
				temp1[1]=5;
			}
			//Left
			//Debug.Log("Left");
			//Debug.Log(Maze[(int)currentPoint.x-1  , (int)currentPoint.y, 1]);
			if(Maze[(int)currentPoint.x-1 , (int)currentPoint.y  , 1]==false)
			{
				temp[0]=Maze[(int)currentPoint.x-2 , (int)currentPoint.y  , 1];
				temp[1]=Maze[(int)currentPoint.x-1 , (int)currentPoint.y+1, 1];
				temp[2]=Maze[(int)currentPoint.x-1 , (int)currentPoint.y  , 1];
				temp[3]=Maze[(int)currentPoint.x-1 , (int)currentPoint.y-1, 1];
				leftD=new Vector2 ((currentPoint.x-1),(currentPoint.y));
				
				if(temp[2]==true || (temp[0]==true && temp[1]==true && temp[3]==true))
				{
					temp1[2]=5;
				}
				else{
					temp1[2]=3;
				}
				if(previousPoint==leftD)//Exception Already Traveled
				{
					temp1[2]=4;
				}
			}
			else{
				temp1[2]=5;
			}
			//Right
			//Debug.Log("Right");
			//Debug.Log(Maze[(int)currentPoint.x  , (int)currentPoint.y+1, 1]);
			if(Maze[(int)currentPoint.x+1 , (int)currentPoint.y  , 1]==false)
			{
				temp[0]=Maze[(int)currentPoint.x+2 , (int)currentPoint.y  , 1];
				temp[1]=Maze[(int)currentPoint.x+1 , (int)currentPoint.y+1, 1];
				temp[2]=Maze[(int)currentPoint.x+1 , (int)currentPoint.y  , 1];
				temp[3]=Maze[(int)currentPoint.x+1 , (int)currentPoint.y-1, 1];
				rightD=new Vector2 ((currentPoint.x+1),(currentPoint.y));
				
				if(temp[2]==true || (temp[0]==true && temp[1]==true && temp[3]==true))
				{
					temp1[3]=5;
				}
				else{
					temp1[3]=3;
				}
				if(previousPoint==rightD)//Exception Already Traveled
				{
					temp1[3]=4;
				}
			}
			else{
				temp1[3]=5;
			}
			//Exceptions
			int[] closeUp= new int [4];
			int[] closeLeft= new int [4];
			for(int i =0; i<4; i++)
			{
				closeUp[i]=(int)(Size.x+Size.y);
				closeLeft[i]=(int)(Size.x+Size.y);
			}
			if(temp1[0]==3)//Shows how close a position is to the end point for an x y perspective
			{//up
				closeLeft[0]=(int)Mathf.Abs((endPoint.x-upD.x));
				closeUp[0]=(int)Mathf.Abs((endPoint.y-upD.y));
			}
			if(temp1[1]==3)
			{//down
				closeLeft[1]=(int)Mathf.Abs((endPoint.x-downD.x));
				closeUp[1]=(int)Mathf.Abs((endPoint.y-downD.y));
			}
			if(temp1[2]==3)
			{//left
				closeLeft[2]=(int)Mathf.Abs((endPoint.x-leftD.x));
				closeUp[2]=(int)Mathf.Abs((endPoint.y-leftD.y));
			}
			if(temp1[3]==3)
			{//right
				closeLeft[3]=(int)Mathf.Abs((endPoint.x-rightD.x));
				closeUp[3]=(int)Mathf.Abs((endPoint.y-rightD.y));
			}
			
			Vector2 Closest = new Vector2 (0,0);//x=left y=up
			int indexOfColosest=0;
			
			float CompareOld;
			float CompareNew;
			for (int i=0; i<4;i++)
			{
				
				if(i==0)
				{
					if((temp1[1]==4 && i==0))
					{
						closeLeft[i]=(int)((float)closeLeft[i]-(float)closeLeft[i]*(float)0.03);
						closeUp[i]=(int)((float)closeUp[i]-(float)closeUp[i]*(float)0.03);
						
						Closest = new Vector2 ((float)closeLeft[i],(float)closeUp[i]);
					}
					else
					{
						Closest = new Vector2 ((float)closeLeft[i],(float)closeUp[i]);
					}
				}
				else
				{
					if((temp1[0]==4 && i==1)||(temp1[1]==4 && i==0)||(temp1[2]==4 && i==3)||(temp1[3]==4 && i==2))
					{
						if(closeLeft[i]!=0&&closeUp[i]!=0)
						{
							closeLeft[i]=(int)((float)closeLeft[i]-(float)closeLeft[i]*(float)0.03);
							closeUp[i]=(int)((float)closeUp[i]-(float)closeUp[i]*(float)0.03);
							
							CompareOld=(Mathf.Pow((Closest.x),(float)2)+Mathf.Pow((Closest.y),(float)2));
							CompareNew=(Mathf.Pow((closeLeft[i]),(float)2)+Mathf.Pow((closeUp[i]),(float)2));
							if(CompareOld > CompareNew)
							{
								Closest=new Vector2 ((float)closeLeft[i],(float)closeUp[i]);
								indexOfColosest=i;
							}
						}
						else
						{
							CompareOld=(Mathf.Pow((Closest.x),(float)2)+Mathf.Pow((Closest.y),(float)2));
							CompareNew=(Mathf.Pow((closeLeft[i]),(float)2)+Mathf.Pow((closeUp[i]),(float)2));
							//CompareNew = (CompareNew - (CompareNew * (float)(0.3)));
							if(CompareOld > CompareNew)
							{
								Closest=new Vector2 ((float)closeLeft[i],(float)closeUp[i]);
								indexOfColosest=i;
							}
						}
					}
					else
					{
						
						CompareOld=(Mathf.Pow((Closest.x),(float)2)+Mathf.Pow((Closest.y),(float)2));
						CompareNew=(Mathf.Pow((closeLeft[i]),(float)2)+Mathf.Pow((closeUp[i]),(float)2));
						
						
						if(CompareOld > CompareNew)
						{
							Closest=new Vector2 ((float)closeLeft[i],(float)closeUp[i]);
							indexOfColosest=i;
						}
					}
				}
			}
			temp1[indexOfColosest]=2;
			
			previousPoint=currentPoint;
			
			int Direction=0;
			for(int i=0; i<4; i++)//Picking the direction to go
			{
				if(temp1[Direction]>temp1[i])
				{
					Direction=i;
				}
			}
			if(Direction==0)
			{
				currentPoint=upD;
			}
			if(Direction==1)
			{
				currentPoint=downD;
			}
			if(Direction==2)
			{
				currentPoint=leftD;
			}
			if(Direction==3)
			{
				currentPoint=rightD;
			}
			
			Maze[(int) currentPoint.x, (int)currentPoint.y, 0]=true;
			
			if(q==100)//REMOVE THIS LATER USE NOW IN CASE OF INFINITE LOOP. THIS WAY I WONT CRASH THE MACHINE ANY MORE
			{
				/*if(Maze[(int)Size.x,(int)Size.y,2]==true)
				{
				}*/
				Debug.Log("Infinite Loop Error in the Path Finder");
				break;
				
			}
			else{
				q++;
			}
			
		}while(currentPoint != endPoint);
		
		
		
	}
	void setUpMazeBorder()
	{
		int xSize = (int) Size.x;
		int ySize = (int) Size.y;
		Maze = new bool [xSize,ySize,2];
		
		for(int y = 0; y < ySize; y++)
		{
			for(int x = 0; x < xSize; x++)
			{
				Maze[x,y,0]=false;
				if(x==(xSize-1)||x==0||y==0||y==(ySize-1))
				{
					Maze[x,y,1]=true;
				}
				else
				{
					Maze[x,y,1]=false;
				}
			}
		}
	}
	void CreateGrid()
	{
		setPlayer();
		setEndPoint();
		for(int y = 0; y < Size.y; y++)
		{
			for(int x = 0; x < Size.x; x++)
			{
				if(Maze[x,y,0]==false)
				{
					Instantiate(WallUnit, new Vector3((x*2),0,(y*2)),Quaternion.identity);
					//Instantiate(WallUnit, new Vector3(((x*2)+1),0,((y*2)+1)),Quaternion.identity);
					//Instantiate(WallUnit, new Vector3((x*2),0,((y*2)+1)),Quaternion.identity);
					//Instantiate(WallUnit, new Vector3(((x*2)+1),0,(y*2)),Quaternion.identity);
				}
				Instantiate(Floor, new Vector3((x*2),-1,(y*2)),Quaternion.identity);
				Instantiate(Floor, new Vector3((x*2),2,(y*2)),Quaternion.identity);
				//Instantiate(RoofUnit, new Vector3(((x*2)+1),3,((y*2)+1)),Quaternion.identity);
				//Instantiate(RoofUnit, new Vector3((x*2),3,((y*2)+1)),Quaternion.identity);
				//Instantiate(RoofUnit, new Vector3(((x*2)+1),3,(y*2)),Quaternion.identity);
			}
		}
		
		placeDoors();
		usedPlayer = Instantiate(Player, new Vector3((StartPoint.x*2),0,(StartPoint.y*2)),Quaternion.identity);
		//Player.transform.position = new Vector3 ((StartPoint.x * 2), 0, (StartPoint.y * 2));
		Instantiate(End, new Vector3((mazeEndPoint.x*2),2,(mazeEndPoint.y*2)),Quaternion.identity);
		insertEnemies();
		insertFurniture();
		if(levelNumber==1)
		{
			double temp = System.DateTime.Now.Millisecond;
			GameTimer.Instance.setStart1(temp);
		}
		else if(levelNumber==2)
		{
			double temp = System.DateTime.Now.Millisecond;
			GameTimer.Instance.setStart2(temp);
		}
		else{
			double temp = System.DateTime.Now.Millisecond;
			GameTimer.Instance.setStart3(temp);
		}
		
	}
	void insertFurniture()
	{
		Vector3 temp;
		Transform nextRoom;
		//Debug.Log(NumberOfRooms);
		int tempPills;
		for(int i=1; i<(NumberOfRooms-1); i++)
		{
			tempPills=NumPillsToAddToRoom(i);
			temp=roomNodes[i];
			//Debug.Log(i);
			if(i==NumberOfRooms)
			{
				//Do nothing. Room will be just a staircase
			}
			else if (temp.z==3)
			{
				//Do nothing. I don't have this type of room
			}
			else if (temp.z==4)
			{
				//Place a room of a 3x3 type
				nextRoom=RoomVariantToUse(3,3,tempPills);
				Instantiate(nextRoom, new Vector3((temp.x*2)+2,0,(temp.y*2)+1),Quaternion.identity);
			}
			else if (temp.z==5)
			{
				//place a room of 4x4 type
				nextRoom=RoomVariantToUse(4,4,tempPills);
				Instantiate(nextRoom, new Vector3((temp.x*2)+2,0,(temp.y*2)+2),Quaternion.identity);
			}
			else if (temp.z==6)
			{
				//place a room of 5x5 type.
				nextRoom=RoomVariantToUse(5,5,tempPills);
				Instantiate(nextRoom, new Vector3((temp.x*2)+2,0,(temp.y*2)+2),Quaternion.identity);
			}
			else
			{
				//Don't place a room
			}
			
		}
		
		//roomNodes[0];
	}
	void insertEnemies()
	{// add item code here
		//add code to create new restrictions in the maze
		//insertFurniture();
		int Rand1;
		int Rand2;
		for(int i=0; i < NumberOfEnemies; i++)
		{
			Rand1= (int)((Random.value * Size.x)%Size.x);
			Rand2= (int) ((Random.value * Size.y)%Size.y);
			//Debug.Log(i);
			if(Maze[Rand1,Rand2,0]==true)
			{
				Maze[Rand1,Rand2,0]=false;
				if(i%3==0)
				{
					if(roomNodes[0].x < Rand1 && (roomNodes[0].x+roomNodes[0].z) > Rand1 && (roomNodes[0].y+roomNodes[0].z)> Rand2 && roomNodes[0].y < Rand2)
					{
						i--;
					}
					else{
						Instantiate(Enemy1, new Vector3(Rand1*2,0,Rand2*2),Quaternion.identity);
					}
				}
				else
				{
					if(roomNodes[0].x < Rand1 && (roomNodes[0].x+roomNodes[0].z) > Rand1 && (roomNodes[0].y+roomNodes[0].z)> Rand2 && roomNodes[0].y < Rand2)
					{
						i--;
					}
					else{
						Instantiate(Enemy1, new Vector3(Rand1*2,0,Rand2*2),Quaternion.identity);
					}
				}
			}
			else{
				i--;
			}
		}
		/*		int Rand1;
		int Rand2;
		for(int i=0; i < NumberOfEnemies; i++)
		{
			Rand1= (int)((Random.value * Size.x)%Size.x);
			Rand2= (int) ((Random.value * Size.y)%Size.y);
			//Debug.Log(i);
			if(Maze[Rand1,Rand2,0]==true)
			{
				if(i%3==0)
				{
					Instantiate(Enemy2, new Vector3(Rand1*2,0,Rand2*2),Quaternion.identity);
				}
				else
				{
					Instantiate(Enemy1, new Vector3(Rand1*2,0,Rand2*2),Quaternion.identity);
				}
			}
			else{
				i--;
			}
		}*/
	}
	//PreRec: The Current Room Number. 
	//PostRec: The Number of Pills for the room; 
	void setUpPills()
	{
		int pills=0;
		//numPillsInRoom
		//numPillsToAddToMaze
		int roomsLeftToFill=NumberOfRooms-2;
		for( int i=0; i < (NumberOfRooms-1); i++)
		{
			if(i==0)
			{
				numPillsInRoom[i]=0;
			}
			else
			{
				if(roomsLeftToFill/numPillsToAddToMaze>1)
				{
					if(roomsLeftToFill/numPillsToAddToMaze>=2)
					{
						numPillsInRoom[i]=2;
						numPillsToAddToMaze=numPillsToAddToMaze-2;
					}
					pills= (int)(1+((0+(Random.value*1000))%2));
					if(numPillsToAddToMaze-pills<0)
					{
						if(numPillsToAddToMaze-pills<0)
						{
							numPillsInRoom[i]=0;
						}
						else
						{
							numPillsInRoom[i]=pills;
							numPillsToAddToMaze=numPillsToAddToMaze-pills;
						}
					}
					else
					{
						numPillsInRoom[i]=pills;
						numPillsToAddToMaze=numPillsToAddToMaze-pills;
					}
				}
				else
				{
					pills= (int)((0+(Random.value*1000))%3);
					if(numPillsToAddToMaze-pills<0)
					{
						if(numPillsToAddToMaze-pills<0)
						{
							numPillsInRoom[i]=0;
						}
						else
						{
							numPillsInRoom[i]=pills;
							numPillsToAddToMaze=numPillsToAddToMaze-pills;
						}
					}
					else
					{
						numPillsInRoom[i]=pills;
						numPillsToAddToMaze=numPillsToAddToMaze-pills;
					}
				}
				roomsLeftToFill=roomsLeftToFill-1;
			}
		}
		//int pills= (int)((0+(Random.value*1000))%3);
	}
	int NumPillsToAddToRoom(int roomNumber){
		int pills=0;
		if(numPillsToAddToMaze>0)
		{
			pills = numPillsInRoom[roomNumber];
		}
		return pills;
		//numPillsToAddToMaze
	}
	//PreRec: The x size and Y size of a room and the number of pills for that room(there can only be up to 2 pills in a room and no less than zero pills in a room.
	//PostRec: The room with the pills as a transform. In the event of not finding the proper size of a room then an enemy will be spawned in the room alone also if invalid pill sizes are inputed
	Transform RoomVariantToUse(int roomSizeX, int roomSizeY, int pills){
		int pickroom=0;
		if(roomSizeX==3 && roomSizeY==3)
		{
			if(pills>2||pills<0)
			{
				return Enemy1;
			}
			else{
				pickroom=(int)((Random.value*1000)%2);
				if(pickroom==0)
				{
					return room3By3Op1[pills];
				}
				else
				{
					return room3By3Op2[pills];
				}
			}
		}
		if(roomSizeX==4 && roomSizeY==4)
		{
			if(pills>2||pills<0)
			{
				return Enemy1;
			}
			else{
				pickroom=(int)((Random.value*1000)%5);
				if(pickroom==0)
				{
					return room4By4Op1[pills];
				}
				else if(pickroom==1)
				{
					return room4By4Op2[pills];
				}
				else if(pickroom==2)
				{
					return room4By4Op3[pills];
				}
				else if(pickroom==3)
				{
					return room4By4Op4[pills];
				}
				else
				{
					return room4By4Op5[pills];
				}
			}
		}
		if(roomSizeX==5 && roomSizeY==5)
		{
			if(pills>2||pills<0)
			{
				return Enemy1;
			}
			else{
				pickroom=(int)((Random.value*1000)%3);
				if(pickroom==0)
				{
					return room5By5Op1[pills];
				}
				else if(pickroom==1)
				{
					return room5By5Op2[pills];
				}
				else
				{
					return room5By5Op3[pills];
				}
			}
		}
		else
		{
			return Enemy1;//Don't Know what To put Here SO I will Put an enemy Here 
		}
		return Enemy2;
	}
	/*Object getPlayer() 
	{
		return usedPlayer;
	}*/
	
	// Update is called once per frame
	void Update () {
		
	}
}
