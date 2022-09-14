using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Bitmap;

public class PolishCalc : MonoBehaviour
{
    public InputField text;
    Stack<PolishNode> head = new Stack<PolishNode>();
    Queue<PolishNode> output = new Queue<PolishNode>();
    string input;
    public Text txt;

    class PolishNode
    {
        public int val;
        public char ch;

        public PolishNode(int val, char ch)
        {
            this.val = val;
            this.ch = ch;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCalc()
    {
        input = text.text;
        int result = 0;
        string outputS = "";
        int i = 0;
        while(i < input.Length)
        {
            switch (input[i])
            {
                case '+':
                    if (head.Count == 0 || ((head.Peek().ch != '*') && (head.Peek().ch != '/') && (head.Peek().ch != '-') && (head.Peek().ch != '+')))
                    {
                        head.Push(new PolishNode(0, input[i]));
                    }
                    else
                    {
                        output.Enqueue(head.Peek());
                        head.Pop();
                        head.Push(new PolishNode(0, input[i]));
                    }
                    break;
                case '-':
                    if (head.Count == 0 || ((head.Peek().ch != '*') && (head.Peek().ch != '/') && (head.Peek().ch != '-') && (head.Peek().ch != '+')))
                    {
                        head.Push(new PolishNode(0, input[i]));
                    }
                    else
                    {
                        output.Enqueue(new PolishNode(0, head.Peek().ch));
                        head.Pop();
                        head.Push(new PolishNode(0, input[i]));
                    }
                    break;
                case '*':
                    if (head.Count == 0 || ((head.Peek().ch != '/') && (head.Peek().ch != '*')))
                    {
                        head.Push(new PolishNode(0, input[i]));
                    }
                    else
                    {
                        output.Enqueue(new PolishNode(0, head.Peek().ch));
                        head.Pop();
                        head.Push(new PolishNode(0, input[i]));
                    }
                    break;
                case '/':
                    if (head.Count == 0 || ((head.Peek().ch != '/') && (head.Peek().ch != '*')))
                    {
                        head.Push(new PolishNode(0, input[i]));
                    }
                    else
                    {
                        output.Enqueue(new PolishNode(0, head.Peek().ch));
                        head.Pop();
                        head.Push(new PolishNode(0, input[i]));
                    }
                    break;
                case '(':
                    head.Push(new PolishNode(0, input[i]));
                    break;
                case ')':
                    while (head.Peek().ch != '(')
                    {
                        output.Enqueue(new PolishNode(0, head.Peek().ch));
                        head.Pop();
                    }
                    head.Pop();
                    break;
                default:
                    while ((i < input.Length) && (input[i] != '*') && (input[i] != '/') && (input[i] != '-') && (input[i] != '+') && (input[i] != '(') && (input[i] != ')') )
                    {
                        result *= 10;
                        result += input[i] - '0';
                        i++;
                    }
                    output.Enqueue(new PolishNode(result, '0'));
                    result = 0;
                    i--;
                    break;
            }
            i++;
        }

        foreach(PolishNode p in head)
        {
            output.Enqueue(new PolishNode(0, head.Peek().ch));
        }

        foreach (PolishNode p in output)
        {
            if (p.ch != '0')
            {
                outputS += p.ch;
            }
            else
            {
                outputS += p.val;
            }
            outputS += ' ';
        }

        text.text = "Reverse polish notation: \n";
        text.text += outputS +'\n';
        result = 0;
        foreach (PolishNode p in output)
        {
            int temp = 0;
            switch (p.ch)
            {
                case '+':
                    result += head.Peek().val;
                    head.Pop();
                    result += head.Peek().val;
                    head.Pop();
                    head.Push(new PolishNode(result, '0'));
                    break;
                case '-':
                    temp = head.Peek().val;
                    head.Pop();
                    result += head.Peek().val;
                    result -= temp;
                    head.Pop();
                    head.Push(new PolishNode(result, '0'));
                    break;
                case '*':
                    result += head.Peek().val;
                    head.Pop();
                    result *= head.Peek().val;
                    head.Pop();
                    head.Push(new PolishNode(result, '0'));
                    break;
                case '/':
                    temp = head.Peek().val;
                    head.Pop();
                    result += head.Peek().val;
                    result /= temp;
                    head.Pop();
                    head.Push(new PolishNode(result, '0'));
                    break;
                default:
                    head.Push(new PolishNode(p.val, '0'));
                    break;
            }
            result = 0;
        }

        /*text.text = "Reverse polish notation: \n";
        text.text += outputS + '\n';
        text.text = "Result:\n";*/
        txt.text = "" + head.Peek().val + '\n';


    }

    public Image make_bw(Image original)
    {

        Image output = new Image(original.width, original.Height);

        for (int i = 0; i < original.Width; i++)
        {

            for (int j = 0; j < original.Height; j++)
            {

                Color c = original.GetPixel(i, j);

                int average = ((c.R + c.B + c.G) / 3);

                if (average < 200)
                    output.SetPixel(i, j, Color.Black);

                else
                    output.SetPixel(i, j, Color.White);

            }
        }

        return output;

    }

}
