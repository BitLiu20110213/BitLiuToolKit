using Hacknet;
using Hacknet.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pathfinder.Executable;
using Pathfinder.Port;
using Pathfinder.Util;
using System;
using System.Collections.Generic;

public class BitLiuTool : BaseExecutable
{
    private bool isComplete = false;
    private bool actionLoaded = false;
    private int BitLiuPort;
    public Color DeepSkyBlue = new Color(0, 191, 255);

    private const string WARNING_TEXT = "OPENING BITLIU BACKDOOR...";
    private const string SUCCESS_LINE1 = "Success";
    private const string SUCCESS_LINE2 = "The Port 213 is opened!";
    
    private float warningPulse = 0.0f;
    private float pulseTimer = 0.0f;
    private float lifetime = 0.0f;
    private Color backgroundColor = Color.Transparent;
    
    private float lineAnimTime1 = -0.9f;
    private int linesToDraw1 = 0;
    private const float lineInterval1 = 0.3f;
    private int maxLines1 = 2;
    private float lineAnimTime2 = -1.5f;
    private int linesToDraw2 = 0;
    private const float lineInterval2 = 0.3f;
    private int maxLines2 = 29;
    private float lineAnimTime3 = -10.2f;
    private int linesToDraw3 = 0;
    private const float lineInterval3 = 0.3f;
    private int maxLines3 = 2;
    private float lineAnimTime4 = -11.9f;
    private int linesToDraw4 = 0;
    private const float lineInterval4 = 0.3f;
    private int maxLines4 = 3;
    private float lineAnimTime5 = -12.8f;
    private int linesToDraw5 = 0;
    private const float lineInterval5 = 0.3f;
    private int maxLines5 = 29;
    private float lineAnimTime6 = -21.5f;
    private int linesToDraw6 = 0;
    private const float lineInterval6 = 0.3f;
    private int maxLines6 = 3;

    public BitLiuTool(Rectangle location, OS operatingSystem, string[] args): base(location, operatingSystem, args)
    {
        ramCost = 200;
        needsProxyAccess = true;
        name = "BitLiuTool";
        IdentifierName = "BitLiuTool";
    }

    public void draw1()
    {
        for (int i = 1; i <= linesToDraw1; i++)
            DrawLine(spriteBatch, new Vector2(25, 150 - 7 * i), new Vector2(25f, 145 - 7 * i), Color.Red, 2f);
    }

    public void draw2()
    {
        for (int i = 1; i <= linesToDraw2; i++)
            DrawLine(spriteBatch, new Vector2(20 + 7 * i, 131), new Vector2(25 + 7 * i, 131), Color.Red, 2f);
    }

    public void draw3()
    {
        for (int i = 1; i <= linesToDraw3; i++)
            DrawLine(spriteBatch, new Vector2(228, 131 + 7 * i), new Vector2(228, 126 + 7 * i), Color.Red, 2f);
    }

    public void draw4()
    {
        for (int i = 1; i <= linesToDraw4; i++)
            DrawLine(spriteBatch, new Vector2(25, 150 + 7 * i), new Vector2(25, 155 + 7 * i), Color.Red, 2f);
    }

    public void draw5()
    {
        for (int i = 1; i <= linesToDraw5; i++)
            DrawLine(spriteBatch, new Vector2(20 + 7 * i, 176), new Vector2(25 + 7 * i, 176), Color.Red, 2f);
    }

    public void draw6()
    {
        for (int i = 1; i <= linesToDraw6; i++)
            DrawLine(spriteBatch, new Vector2(228, 176 - 7 * i), new Vector2(228, 181 - 7 * i), Color.Red, 2f);
    }

    public override void Draw(float t)
    {
        drawOutline();
        drawTarget("app:");
        Rectangle drawArea = Utils.InsetRectangle(new Rectangle(this.bounds.X, this.bounds.Y + Module.PANEL_HEIGHT, this.bounds.Width, this.bounds.Height - Module.PANEL_HEIGHT), 2);
        spriteBatch.Draw(Utils.white, drawArea, backgroundColor);
        if (!isExiting)
        {
            if (!isComplete)
            {
                RenderedRectangle.doRectangle(bounds.X + 30, bounds.Y + 90, 50, 25, Color.DeepSkyBlue);
                RenderedRectangle.doRectangle(bounds.X + 170, bounds.Y + 90, 50, 25, Color.DeepSkyBlue);
                DrawWarningBanner();
                if (lifetime >= 0.3f)
                {
                    DrawLine(spriteBatch, new Vector2(30, 152), new Vector2(25, 152), Color.Red, 2f);
                    if (lifetime >= 0.6f)
                    {
                        DrawLine(spriteBatch, new Vector2(25, 150), new Vector2(25, 145), Color.Red, 2f);
                        if (lifetime >= 0.9f)
                        {
                            draw1();
                            if (lifetime >= 1.5f)
                            {
                                draw2();
                                if (lifetime >= 10.2f)
                                {
                                    draw3();
                                    if (lifetime >= 10.8f)
                                    {
                                        DrawLine(spriteBatch, new Vector2(230, 147), new Vector2(230, 152), Color.Red, 2f);
                                        if (lifetime >= 11.3f)
                                        {
                                            DrawLine(spriteBatch, new Vector2(230, 152), new Vector2(225, 152), Color.Red, 2f);
                                            if (lifetime >= 11.6f)
                                            {
                                                DrawLine(spriteBatch, new Vector2(25, 157), new Vector2(30, 157), Color.Red, 2f);
                                                if (lifetime >= 11.9f)
                                                {
                                                    draw4();
                                                    if (lifetime >= 12.8f)
                                                    {
                                                        draw5();
                                                        if (lifetime >= 21.5f)
                                                        {
                                                            draw6();
                                                            if (lifetime >= 22.4f)
                                                            {
                                                                DrawLine(spriteBatch, new Vector2(223, 155), new Vector2(228, 155), Color.Red, 2f);
                                                                if (lifetime >= 22.7f)
                                                                {
                                                                    DrawLine(spriteBatch, new Vector2(82, 150), new Vector2(171, 150), Color.Yellow, 2f);
                                                                    DrawLine(spriteBatch, new Vector2(82, 155), new Vector2(171, 155), Color.Yellow, 2f);
                                                                    DrawLine(spriteBatch, new Vector2(82, 160), new Vector2(171, 160), Color.Yellow, 2f);
                                                                    backgroundColor = Color.Transparent;
                                                                    if (lifetime >= 23.0f)
                                                                    {
                                                                        DrawWarningBanner();
                                                                        DrawSuccessBanner();
                                                                        if (lifetime >= 25.0f)
                                                                        {
                                                                            isComplete = true;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        base.Draw(t);
    }
    public void DrawLine(
      SpriteBatch spriteBatch,
      Vector2 start,
      Vector2 end,
      Color color,
      float thickness = 1f)
    {
        // �����߶�����
        Vector2 direction = end - start;
        float length = direction.Length();

        // ������ת�Ƕ�
        float angle = (float)Math.Atan2(direction.Y, direction.X);

        // ʹ��һ���ذ�ɫ�����������ֱ��
        spriteBatch.Draw(
            Utils.white,                // ����
            start,                      // λ��
            null,                       // Դ����
            color,                      // ��ɫ
            angle,                      // ��ת
            Vector2.Zero,               // ԭ��
            new Vector2(length, thickness), // ���ţ����Ⱥʹ�ϸ��
            SpriteEffects.None,         // ����Ч
            0f                          // �㼶
        );
    }

    private void DrawWarningBanner()
    {
        Rectangle drawArea = Utils.InsetRectangle(new Rectangle(this.bounds.X, this.bounds.Y + Module.PANEL_HEIGHT, this.bounds.Width, this.bounds.Height - Module.PANEL_HEIGHT), 2);
        int bannerHeight = 30;
        int bannerY = drawArea.Y + 5;

        // ȷ���ڴ��ڱ߽���
        bannerY = Math.Max(bannerY, drawArea.Y + 5);
        bannerHeight = Math.Min(bannerHeight, drawArea.Height - 10);

        // �����ɫ����ɫ��
        float blink = (float)Math.Sin(warningPulse * 10f) * 0.5f + 0.5f;
        Color bannerColor = new Color(0, 100, 0);

        // ���ƺ������
        spriteBatch.Draw(Utils.white,
            new Rectangle(drawArea.X, bannerY, drawArea.Width, bannerHeight),
            bannerColor);

        // ���ƺ���ı� - ������ʾ
        Vector2 textSize = GuiData.tinyfont.MeasureString(WARNING_TEXT);
        Vector2 textPos = new Vector2(
            drawArea.X + (drawArea.Width - textSize.X) / 2,
            bannerY + (bannerHeight - textSize.Y) / 2
        );

        spriteBatch.DrawString(GuiData.tinyfont, WARNING_TEXT,
            textPos,
            Color.White);
    }

    private void DrawSuccessBanner()
    {
        // �����������
        Rectangle drawArea = Utils.InsetRectangle(
            new Rectangle(this.bounds.X, this.bounds.Y + Module.PANEL_HEIGHT, this.bounds.Width, this.bounds.Height - Module.PANEL_HEIGHT), 2);

        int bannerHeight = 60;
        int bannerY = drawArea.Bottom - bannerHeight;
        int bannerX = drawArea.X;
        int bannerWidth = drawArea.Width;

        // ��̬��ɫ������������΢���嶯��
        float pulse = 0.6f + 0.4f * (float)Math.Sin(this.pulseTimer * 5.0);
        Color bannerColor = new Color(0, (int)(120 * pulse), 0);

        // ���ƺ������
        spriteBatch.Draw(Utils.white, new Rectangle(bannerX, bannerY, bannerWidth, bannerHeight), bannerColor);

        // ������������ߴ�
        Vector2 titleSize = GuiData.font.MeasureString(SUCCESS_LINE1);
        Vector2 subtitleSize = GuiData.smallfont.MeasureString(SUCCESS_LINE2);

        // ���㴹ֱ������ʼY
        float totalTextHeight = titleSize.Y + subtitleSize.Y;
        float textStartY = bannerY + (bannerHeight - totalTextHeight) / 2f;

        // ���л���������
        Vector2 titlePos = new Vector2(
            bannerX + (bannerWidth - titleSize.X) / 2f,
            textStartY
        );
        spriteBatch.DrawString(GuiData.font, SUCCESS_LINE1, titlePos, Color.White);

        // ���л��Ƹ�����
        Vector2 subtitlePos = new Vector2(
            bannerX + (bannerWidth - subtitleSize.X) / 2f,
            textStartY + titleSize.Y
        );
        spriteBatch.DrawString(GuiData.smallfont, SUCCESS_LINE2, subtitlePos, Color.White);
    }

    public override void LoadContent()
    {
        Computer c = ComputerLookup.FindByIp(targetIP);
        BitLiuPort = c.GetDisplayPortNumberFromCodePort(213);
        bool isPortExisit = PortDetect.IsHasPort(c, BitLiuPort);

        if(Args.Length < 2)
        {
            os.write("�޶˿ں��ṩ");
            os.write("ִ��ʧ��");
            needsRemoval = true;

            return;
        }
        else if (Int32.Parse(Args[1]) != BitLiuPort || !isPortExisit)
        {
            os.write("Ŀ��˿�δ����");
            os.write("ִ��ʧ��");
            needsRemoval = true;
            return;
        }
        if (actionLoaded == false)
        {
            Console.WriteLine("Loading BitLiuTool Actions...");
            RunnableConditionalActions.LoadIntoOS("BLTA.xml", os);
            actionLoaded = true;
        }
        base.LoadContent();
    }

    public override void Update(float t)
    {
        lifetime += t;
        if (linesToDraw1 < maxLines1)
        {
            lineAnimTime1 += t;
            if (lineAnimTime1 >= lineInterval1)
            {
                lineAnimTime1 -= lineInterval1;
                linesToDraw1++;
            }
        }
        if (linesToDraw2 < maxLines2)
        {
            lineAnimTime2 += t;
            if (lineAnimTime2 >= lineInterval2)
            {
                lineAnimTime2 -= lineInterval2;
                linesToDraw2++;
            }
        }
        if (linesToDraw3 < maxLines3)
        {
            lineAnimTime3 += t;
            if (lineAnimTime3 >= lineInterval3)
            {
                lineAnimTime3 -= lineInterval3;
                linesToDraw3++;
            }
        }
        if (linesToDraw4 < maxLines4)
        {
            lineAnimTime4 += t;
            if (lineAnimTime4 >= lineInterval4)
            {
                lineAnimTime4 -= lineInterval4;
                linesToDraw4++;
            }
        }
        if (linesToDraw5 < maxLines5)
        {
            lineAnimTime5 += t;
            if (lineAnimTime5 >= lineInterval5)
            {
                lineAnimTime5 -= lineInterval5;
                linesToDraw5++;
            }
        }
        if (linesToDraw6 < maxLines6)
        {
            lineAnimTime6 += t;
            if (lineAnimTime6 >= lineInterval6)
            {
                lineAnimTime6 -= lineInterval6;
                linesToDraw6++;
            }
        }
        if (isComplete == true)
        {
            var c = Programs.getComputer(os, targetIP);
            c.openPort(BitLiuPort, os.thisComputer.ip);
            os.write("BitLiu Backdoor �˿��ѳɹ��򿪣�");
            c.openPort(188, os.thisComputer.ip);
            os.write("188�˿��ѳɹ��򿪣�");
            c.openPort(211, os.thisComputer.ip);
            os.write("211�˿��ѳɹ��򿪣�");
            c.openPort(3659, os.thisComputer.ip);
            os.write("3659�˿��ѳɹ��򿪣�");
            isExiting = true;
        }
        base.Update(t);
    }

    public class PortDetect
    {
        public static bool IsHasPort(Computer computer, int port)
        {
            Dictionary<string, PortState> PortDict = computer.GetPortStateDict();

            foreach (var kvp in PortDict)
            {
                if (kvp.Value.PortNumber == port)
                {
                    return true;
                }
            }
            return false;
        }
    }
}