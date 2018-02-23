' MemoryGame.vb
' Name: Jason Yue
' Student Number: 492329
' CMPT360 Spring 2015
' Assignment #5
' Memory Matching Card Game
Option Explicit On
' To organize the objects of Cards
Namespace Card
    ' Functions for what's on the card
    Class Face
        Private face As String
        Private value As String
        ' Constructor for variables
        Public Sub New(val As String, num As String)
            face = val
            value = num
        End Sub
        ' Accessor for face variable
        Public Function faceVal()
            Return face
        End Function
        ' Mutator for face variable
        Public Sub setFace(val As String)
            face = val
        End Sub
        ' Accessor for value variable
        Public Function valueVal()
            Return value
        End Function
    End Class
End Namespace
' This module re-creates the classic memory matching card game.
' 16 cards are flipped down (represented by '*') and the user can flip 2 up 
' at a time to reveal their values by entering their coordinates.
' If the card values match, they stay flipped up and the match count goes up;
' if not, they are flipped back down.
Module MemoryGame
    Sub Main()
        Dim matchCount As Integer = 0, firstInput As String = "", secondInput As String = ""
        ' The extremely laborious object creation process
        Dim card1 As Card.Face : card1 = New Card.Face("0", "1")
        Dim card2 As Card.Face : card2 = New Card.Face("0", "2")
        Dim card3 As Card.Face : card3 = New Card.Face("0", "3")
        Dim card4 As Card.Face : card4 = New Card.Face("0", "4")
        Dim card5 As Card.Face : card5 = New Card.Face("0", "5")
        Dim card6 As Card.Face : card6 = New Card.Face("0", "6")
        Dim card7 As Card.Face : card7 = New Card.Face("0", "7")
        Dim card8 As Card.Face : card8 = New Card.Face("0", "8")
        Dim card9 As Card.Face : card9 = New Card.Face("0", "1")
        Dim card10 As Card.Face : card10 = New Card.Face("0", "2")
        Dim card11 As Card.Face : card11 = New Card.Face("0", "3")
        Dim card12 As Card.Face : card12 = New Card.Face("0", "4")
        Dim card13 As Card.Face : card13 = New Card.Face("0", "5")
        Dim card14 As Card.Face : card14 = New Card.Face("0", "6")
        Dim card15 As Card.Face : card15 = New Card.Face("0", "7")
        Dim card16 As Card.Face : card16 = New Card.Face("0", "8")
        ' Object array containing cards (aka. a deck of cards)
        Dim myArray = New Object(,) {{card1, card2, card3, card4}, {card5, card6, card7, card8},
                                     {card9, card10, card11, card12}, {card13, card14, card15, card16}}
        NewSquare(myArray)
        Console.WriteLine("MemoryGame.vb | By: Jason Yue | Assignment #5")
        Console.WriteLine("Welcome to the Memory Matching Card Game!")
        Display(myArray)
        ' 16 cards divided by 2 same cards = 8 possible matches
        While (matchCount < 8)
            Console.Write("Pick your first card! ")
            firstInput = Console.ReadLine()
            ' Input error checking
            While (CoordCheck(firstInput))
                Console.Write("Invalid input! Please type the coordinates in the form of 'row,column': ")
                firstInput = Console.ReadLine()
            End While
            ' Check if card is faced up or down
            While (VisibilityCheck(firstInput, myArray))
                Console.Write("You already picked that card! Pick again! ")
                firstInput = Console.ReadLine()
                ' Input error checking
                While (CoordCheck(firstInput))
                    Console.Write("Invalid input! Please type the coordinates in the form of 'row,column': ")
                    firstInput = Console.ReadLine()
                End While
            End While
            ' Value of card should be assigned
            Dim firstCard As String = PrintCoord(firstInput, myArray)
            Display(myArray)
            Console.Write("Pick your second card! ")
            secondInput = Console.ReadLine()
            ' Input error checking
            While (CoordCheck(secondInput))
                Console.Write("Invalid input! Please type the coordinates in the form of 'row,column': ")
                secondInput = Console.ReadLine()
            End While
            ' Check if card is faced up or down
            While (VisibilityCheck(secondInput, myArray))
                Console.Write("You already picked that card! Pick again! ")
                secondInput = Console.ReadLine()
                ' Input error checking
                While (CoordCheck(secondInput))
                    Console.Write("Invalid input! Please type the coordinates in the form of 'row,column': ")
                    secondInput = Console.ReadLine()
                End While
            End While
            ' Value of card should be assigned
            Dim secondCard As String = PrintCoord(secondInput, myArray)
            Display(myArray)
            ' Check both values of cards
            If (CompareCoord(firstCard, secondCard)) Then
                matchCount += 1
                Console.WriteLine(matchCount.ToString + " Match(s) Found!")
            Else
                Console.WriteLine("Invalid Match!")
                ' Flip down card
                HideCard(firstInput, secondInput, myArray)
                Display(myArray)
            End If
        End While
        Console.WriteLine("Congratulations! You managed to solve the Memory Game!")
        Console.ReadKey()
    End Sub
    ' Shuffles the 2D Object array as if it were a 1D Object array
    Sub NewSquare(myArray(,) As Object)
        ' Initiate random number generator
        Randomize()
        For i As Integer = 15 To 1 Step -1
            Dim index As Integer = CInt(Math.Floor((i) * Rnd()))
            Dim tmp As Object = myArray(Int((i - 1) / 4), Int((i - 1) Mod 4))
            myArray(Int((i - 1) / 4), Int((i - 1) Mod 4)) = myArray(Int(index / 4), Int(index Mod 4))
            myArray(Int(index / 4), Int(index Mod 4)) = tmp
        Next
    End Sub
    ' Outputs the updated game "board"
    Sub Display(myArray(,) As Object)
        Console.WriteLine("       1  2  3  4")
        Console.WriteLine("     -------------")
        For i As Integer = 0 To 3
            Console.Write(" " + (i + 1).ToString + "  |  ")
            For j As Integer = 0 To 3
                ' May write * or card value
                Console.Write(Value(myArray(i, j)) + "  ")
            Next
            Console.WriteLine()
        Next
    End Sub
    ' "Flip" card and return the numerical value of card as string
    Function PrintCoord(input As String, myArray(,) As Object)
        Dim coord As String() = input.Split(",")
        ' "Flips card essentially
        myArray(Int(coord(0)) - 1, Int(coord(1)) - 1).setFace("1")
        ' Will return card value
        Return Value(myArray(Int(coord(0)) - 1, Int(coord(1)) - 1))
    End Function
    ' "Flip" down both cards
    Sub HideCard(input1 As String, input2 As String, myArray(,) As Object)
        Dim coord1 As String() = input1.Split(",")
        Dim coord2 As String() = input2.Split(",")
        myArray(Int(coord1(0)) - 1, Int(coord1(1)) - 1).setFace("0")
        myArray(Int(coord2(0)) - 1, Int(coord2(1)) - 1).setFace("0")
    End Sub
    ' Representative for card value (face down or up)
    Function Value(x As Card.Face)
        If (x.faceVal <> "0") Then
            Return x.valueVal
        Else
            Return "*"
        End If
    End Function
    ' Check if the card values are equal
    Function CompareCoord(first As String, second As String)
        If (first.Equals(second)) Then
            Return True
        Else
            Return False
        End If
    End Function
    ' Error checking for Coordinates
    ' Coordinates must be within the 4x4 grid and valid (1,1)
    Function CoordCheck(input As String)
        Try
            Dim coord As String() = input.Split(",")
            Dim xCoord As Integer = Int(coord(0)) - 1
            Dim yCoord As Integer = Int(coord(1)) - 1
            If (coord.Length <> 2) Then
                Throw New Exception("Invalid Coordinate Format!")
            End If
            If ((xCoord > 3) Or (xCoord < 0) Or (yCoord > 3) Or (yCoord < 0)) Then
                Throw New Exception("Coordinate Out of Grid!")
            End If
            Return False
        Catch e As Exception
            Console.WriteLine(e.Message)
            Return True
        End Try
    End Function
    ' Check if the card is "faced" up or down
    Function VisibilityCheck(input As String, myArray(,) As Object)
        Dim coord As String() = input.Split(",")
        If (myArray(Int(coord(0)) - 1, Int(coord(1) - 1)).faceVal <> "0") Then
            Return True
        Else
            Return False
        End If
    End Function
End Module
