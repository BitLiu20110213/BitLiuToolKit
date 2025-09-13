using Hacknet;
using Hacknet.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pathfinder.Executable;
using Pathfinder.Port;
using Pathfinder.Util;
using System;
using System.Collections.Generic;

public class SSLFastTool : BaseExecutable
{
    private bool isComplete = false;
    private int SSLPort;
    private int SSHPort;
    private int WebPort;
    public Color DeepSkyBlue = new Color(0, 191, 255);

    private const string SUCCESS_LINE1 = "Success";
    private const string SUCCESS_LINE2 = "The Port 443 is opened!";
    private float pulseTimer = 0.0f;
    private Color backgroundColor = Color.Transparent;

    public SSLFastTool(Rectangle location, OS operatingSystem, string[] args): base(location, operatingSystem, args)
    {
        ramCost = 200;
        needsProxyAccess = true;
        name = "SSLFastTool";
        IdentifierName = "SSLFastTool";
    }

    public override void Draw(float t)
    {
        drawOutline();
        drawTarget("app:");
        Rectangle drawArea = Utils.InsetRectangle(new Rectangle(this.bounds.X, this.bounds.Y + Module.PANEL_HEIGHT, this.bounds.Width, this.bounds.Height - Module.PANEL_HEIGHT), 2);
        spriteBatch.Draw(Utils.white, drawArea, backgroundColor);

        // 坐标系原点（圆心）整体左移50，上移30单位
        Vector2 origin = new Vector2(drawArea.X + drawArea.Width / 2 - 50, drawArea.Y + drawArea.Height / 2 - 30);

        // 1. 绘制平面直角坐标系（围绕 origin）
        DrawLine(spriteBatch, new Vector2(origin.X, drawArea.Y), new Vector2(origin.X, drawArea.Bottom), Color.Gray, 2f);
        DrawLine(spriteBatch, new Vector2(drawArea.X, origin.Y), new Vector2(drawArea.Right, origin.Y), Color.Gray, 2f);

        // 2. 绘制圆（分段数进一步减少，加快速度）
        int radius = 50;
        int circleSegments = 30; // 再快一倍
        float twoPi = (float)(Math.PI * 2);
        for (int i = 0; i < circleSegments; i++)
        {
            float theta1 = twoPi * i / circleSegments;
            float theta2 = twoPi * (i + 1) / circleSegments;
            float cos1 = (float)Math.Cos(theta1);
            float sin1 = (float)Math.Sin(theta1);
            float cos2 = (float)Math.Cos(theta2);
            float sin2 = (float)Math.Sin(theta2);
            Vector2 p1 = origin + new Vector2(cos1, sin1) * radius;
            Vector2 p2 = origin + new Vector2(cos2, sin2) * radius;
            DrawLine(spriteBatch, p1, p2, Color.LightGray, 1f);
        }

        // 3. 绿色点周期震荡（圆周运动点）
        float time = (float)os.timer;
        float angle = time % twoPi;
        float cosAngle = (float)Math.Cos(angle);
        float sinAngle = (float)Math.Sin(angle);
        Vector2 greenPoint = origin + new Vector2(cosAngle, sinAngle) * radius;
        spriteBatch.Draw(Utils.white, new Rectangle((int)greenPoint.X - 4, (int)greenPoint.Y - 4, 8, 8), Color.LimeGreen);

        // 4. 绘制运动轨迹（分段数进一步减少，加快速度）
        int trailSegments = 30;
        for (int i = 0; i < trailSegments; i++)
        {
            float a1 = (time - i * 0.1f) % twoPi; // 间隔加大
            float a2 = (time - (i + 1) * 0.1f) % twoPi;
            Vector2 pt1 = origin + new Vector2((float)Math.Cos(a1), (float)Math.Sin(a1)) * radius;
            Vector2 pt2 = origin + new Vector2((float)Math.Cos(a2), (float)Math.Sin(a2)) * radius;
            DrawLine(spriteBatch, pt1, pt2, Color.LimeGreen * (1f - i / (float)trailSegments), 2f);
        }

        // 5. 正弦余弦动态图像部分保持不变，但采样点减少
        int minPlotWidth = 120;
        int plotAreaX = drawArea.X + radius * 2 + 40;
        if (plotAreaX < drawArea.X + 20) plotAreaX = drawArea.X + 20;
        int plotAreaWidth = Math.Max(minPlotWidth, drawArea.Right - plotAreaX - 10);
        int plotAreaHeight = drawArea.Height - 40;
        Rectangle plotArea = new Rectangle(plotAreaX, drawArea.Y + 20, plotAreaWidth, plotAreaHeight);

        if (plotArea.Width > 40 && plotArea.Height > 40)
        {
            spriteBatch.Draw(Utils.white, plotArea, new Color(10, 10, 10, 200));
            int halfH = (plotArea.Height - 10) / 2;
            Rectangle sinRect = new Rectangle(plotArea.X + 10, plotArea.Y + 5, plotArea.Width - 20, halfH);
            Rectangle cosRect = new Rectangle(plotArea.X + 10, plotArea.Y + 5 + halfH + 5, plotArea.Width - 20, halfH);

            DrawLine(spriteBatch, new Vector2(sinRect.X, sinRect.Y), new Vector2(sinRect.Right, sinRect.Y), Color.DimGray, 1f);
            DrawLine(spriteBatch, new Vector2(sinRect.X, sinRect.Bottom), new Vector2(sinRect.Right, sinRect.Bottom), Color.DimGray, 1f);
            DrawLine(spriteBatch, new Vector2(cosRect.X, cosRect.Y), new Vector2(cosRect.Right, cosRect.Y), Color.DimGray, 1f);
            DrawLine(spriteBatch, new Vector2(cosRect.X, cosRect.Bottom), new Vector2(cosRect.Right, cosRect.Bottom), Color.DimGray, 1f);

            Vector2 sinMidLeft = new Vector2(sinRect.X, sinRect.Y + sinRect.Height / 2f);
            Vector2 sinMidRight = new Vector2(sinRect.Right, sinRect.Y + sinRect.Height / 2f);
            DrawLine(spriteBatch, sinMidLeft, sinMidRight, Color.Gray * 0.8f, 1f);

            Vector2 cosMidLeft = new Vector2(cosRect.X, cosRect.Y + cosRect.Height / 2f);
            Vector2 cosMidRight = new Vector2(cosRect.Right, cosRect.Y + cosRect.Height / 2f);
            DrawLine(spriteBatch, cosMidLeft, cosMidRight, Color.Gray * 0.8f, 1f);

            // 采样点进一步减少
            int sampleCount = Math.Min(40, Math.Max(10, sinRect.Width));
            float anglePerPixel = 0.12f;
            float centerOffset = sampleCount / 2f;

            float sinAmplitude = sinRect.Height * 0.4f;
            Vector2 prevPoint = Vector2.Zero;
            for (int i = 0; i < sampleCount; i++)
            {
                float phase = angle + (i - centerOffset) * anglePerPixel;
                float v = (float)Math.Sin(phase);
                float x = sinRect.X + i * (sinRect.Width / (float)sampleCount);
                float y = sinRect.Y + sinRect.Height / 2f - v * sinAmplitude;
                Vector2 curr = new Vector2(x, y);
                if (i > 0)
                {
                    DrawLine(spriteBatch, prevPoint, curr, Color.OrangeRed, 2f);
                }
                prevPoint = curr;
            }

            float cosAmplitude = cosRect.Height * 0.4f;
            prevPoint = Vector2.Zero;
            for (int i = 0; i < sampleCount; i++)
            {
                float phase = angle + (i - centerOffset) * anglePerPixel;
                float v = (float)Math.Cos(phase);
                float x = cosRect.X + i * (cosRect.Width / (float)sampleCount);
                float y = cosRect.Y + cosRect.Height / 2f - v * cosAmplitude;
                Vector2 curr = new Vector2(x, y);
                if (i > 0)
                {
                    DrawLine(spriteBatch, prevPoint, curr, DeepSkyBlue, 2f);
                }
                prevPoint = curr;
            }

            float markerX = sinRect.X + centerOffset * (sinRect.Width / (float)sampleCount);
            float sinMarkerY = sinRect.Y + sinRect.Height / 2f - (float)Math.Sin(angle) * sinAmplitude;
            float cosMarkerY = cosRect.Y + cosRect.Height / 2f - (float)Math.Cos(angle) * cosAmplitude;
            spriteBatch.Draw(Utils.white, new Rectangle((int)markerX - 4, (int)sinMarkerY - 4, 8, 8), Color.OrangeRed);
            spriteBatch.Draw(Utils.white, new Rectangle((int)markerX - 4, (int)cosMarkerY - 4, 8, 8), DeepSkyBlue);

            Vector2 indicatorStart = origin + new Vector2(cosAngle, sinAngle) * radius;
            Vector2 indicatorEnd = new Vector2(markerX, sinMarkerY);
            DrawLine(spriteBatch, indicatorStart, indicatorEnd, Color.White * 0.15f, 1f);
        }

        // 6. 左下方绘制两条进度条（每条7.5秒跑满，间隔10单位，右端缩短20单位，串行运行，跑满后保持满状态）
        int barFullWidth = 160;
        int barWidth = barFullWidth - 20; // 右端缩短20
        int barHeight = 12;
        int barMargin = 10;
        int barX = drawArea.X + 5;
        int barY1 = drawArea.Bottom - barHeight * 2 - barMargin - 10;
        int barY2 = drawArea.Bottom - barHeight - 10;

        // 串行进度逻辑，跑满后保持满状态（修正：只在程序启动后开始计时，且不循环）
        float progress1 = 0f, progress2 = 0f;
        float maxTime1 = 7.5f;
        float maxTime2 = 7.5f;
        float elapsed = lifetime; // 用 lifetime 记录程序运行时间

        if (elapsed < maxTime1)
        {
            // 第一条进度条跑
            progress1 = elapsed / maxTime1;
            progress2 = 0f;
        }
        else if (elapsed < maxTime1 + maxTime2)
        {
            // 第一条满，第二条跑
            progress1 = 1f;
            progress2 = (elapsed - maxTime1) / maxTime2;
        }
        else
        {
            // 两条都满
            progress1 = 1f;
            progress2 = 1f;
        }

        // 背景条
        spriteBatch.Draw(Utils.white, new Rectangle(barX, barY1, barWidth, barHeight), Color.DimGray * 0.5f);
        spriteBatch.Draw(Utils.white, new Rectangle(barX, barY2, barWidth, barHeight), Color.DimGray * 0.5f);

        // 前景进度
        spriteBatch.Draw(Utils.white, new Rectangle(barX, barY1, (int)(barWidth * progress1), barHeight), Color.LimeGreen);
        spriteBatch.Draw(Utils.white, new Rectangle(barX, barY2, (int)(barWidth * progress2), barHeight), Color.DeepSkyBlue);

        // 进度条边框
        DrawLine(spriteBatch, new Vector2(barX, barY1), new Vector2(barX + barWidth, barY1), Color.Gray, 1f);
        DrawLine(spriteBatch, new Vector2(barX, barY1 + barHeight), new Vector2(barX + barWidth, barY1 + barHeight), Color.Gray, 1f);
        DrawLine(spriteBatch, new Vector2(barX, barY2), new Vector2(barX + barWidth, barY2), Color.Gray, 1f);
        DrawLine(spriteBatch, new Vector2(barX, barY2 + barHeight), new Vector2(barX + barWidth, barY2 + barHeight), Color.Gray, 1f);

        if (lifetime >= 16f)
        {
            DrawSuccessBanner();
            if (lifetime >= 20f)
            {
                isComplete = true;
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
        // 计算线段向量
        Vector2 direction = end - start;
        float length = direction.Length();

        // 计算旋转角度
        float angle = (float)Math.Atan2(direction.Y, direction.X);

        // 使用一像素白色纹理拉伸绘制直线
        spriteBatch.Draw(
            Utils.white,                // 纹理
            start,                      // 位置
            null,                       // 源矩形
            color,                      // 颜色
            angle,                      // 旋转
            Vector2.Zero,               // 原点
            new Vector2(length, thickness), // 缩放（长度和粗细）
            SpriteEffects.None,         // 无特效
            0f                          // 层级
        );
    }

    private void DrawSuccessBanner()
    {
        // 计算绘制区域
        Rectangle drawArea = Utils.InsetRectangle(
            new Rectangle(this.bounds.X, this.bounds.Y + Module.PANEL_HEIGHT, this.bounds.Width, this.bounds.Height - Module.PANEL_HEIGHT), 2);

        int bannerHeight = 60;
        int bannerY = drawArea.Bottom - bannerHeight;
        int bannerX = drawArea.X;
        int bannerWidth = drawArea.Width;

        // 动态绿色背景，带有轻微脉冲动画
        float pulse = 0.6f + 0.4f * (float)Math.Sin(this.pulseTimer * 5.0);
        Color bannerColor = new Color(0, (int)(120 * pulse), 0);

        // 绘制横幅背景
        spriteBatch.Draw(Utils.white, new Rectangle(bannerX, bannerY, bannerWidth, bannerHeight), bannerColor);

        // 计算主副标题尺寸
        Vector2 titleSize = GuiData.font.MeasureString(SUCCESS_LINE1);
        Vector2 subtitleSize = GuiData.smallfont.MeasureString(SUCCESS_LINE2);

        // 计算垂直居中起始Y
        float totalTextHeight = titleSize.Y + subtitleSize.Y;
        float textStartY = bannerY + (bannerHeight - totalTextHeight) / 2f;

        // 居中绘制主标题
        Vector2 titlePos = new Vector2(
            bannerX + (bannerWidth - titleSize.X) / 2f,
            textStartY
        );
        spriteBatch.DrawString(GuiData.font, SUCCESS_LINE1, titlePos, Color.White);

        // 居中绘制副标题
        Vector2 subtitlePos = new Vector2(
            bannerX + (bannerWidth - subtitleSize.X) / 2f,
            textStartY + titleSize.Y
        );
        spriteBatch.DrawString(GuiData.smallfont, SUCCESS_LINE2, subtitlePos, Color.White);
    }

    public override void LoadContent()
    {
        Computer c = ComputerLookup.FindByIp(targetIP);
        SSLPort = c.GetDisplayPortNumberFromCodePort(443);
        SSHPort = c.GetDisplayPortNumberFromCodePort(22);
        WebPort = c.GetDisplayPortNumberFromCodePort(80);
        bool isPortExisit = PortDetect.IsHasPort(c, SSLPort);

        if (Args.Length < 2)
        {
            os.write("无端口号提供");
            os.write("执行失败");
            needsRemoval = true;
            
            return;
        }
        else if (Int32.Parse(Args[1]) != SSLPort || !isPortExisit)
        {
            os.write("目标端口未开放");
            os.write("执行失败");
            needsRemoval = true;
            return;
        }
        if (!c.isPortOpen(SSHPort))
        {
            os.write("22端口未开启");
            os.write("执行失败");
            needsRemoval = true;
            return;
        }
        if (!c.isPortOpen(WebPort))
        {
            os.write("80端口未开启");
            os.write("执行失败");
            needsRemoval = true;
            return;
        }
        base.LoadContent();
    }
    private float lifetime = 0f;
    public override void Update(float t)
    {
        lifetime += t;
        if (isComplete == true)
        {
            var c = Programs.getComputer(os, targetIP);
            c.openPort(SSLPort, os.thisComputer.ip);
            os.write("SSL端口已成功打开！");
            c.openPort(25, os.thisComputer.ip);
            os.write("25端口已成功打开！");
            c.closePort(22, os.thisComputer.ip);
            os.write("22端口已关闭！");
            c.closePort(80, os.thisComputer.ip);
            os.write("80端口已关闭！");
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