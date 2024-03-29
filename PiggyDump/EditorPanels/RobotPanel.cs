﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Descent2Workshop.Transactions;
using LibDescent.Data;
using LibDescent.Edit;

namespace Descent2Workshop.EditorPanels
{
    public partial class RobotPanel : UserControl
    {
        private bool isLocked = false;
        private Robot robot;
        private int robotid;

        //Name lists for drops
        private string[] RobotNames;
        private string[] PowerupNames;

        //to assist with my sanity
        private TextBox[] FieldOfViewControls = new TextBox[5];
        private TextBox[] FireWaitControls = new TextBox[5];
        private TextBox[] FireWait2Controls = new TextBox[5];
        private TextBox[] TurnTimeControls = new TextBox[5];
        private TextBox[] MaxSpeedControls = new TextBox[5];
        private TextBox[] CircleDistControls = new TextBox[5];
        private TextBox[] FireCountControls = new TextBox[5];
        private TextBox[] EvadeSpeedControls = new TextBox[5];

        //HXM file for showing HXM data
        private EditorHXMFile hxmFile;

        //Transaction related data
        private TransactionManager transactionManager;
        private int tabPage;

        public RobotPanel(TransactionManager transactionManager, int tabPage)
        {
            InitializeComponent();
            //hey can any c# experts tell me what obvious feature I'm missing here, and if it exists, why I can't just do something like
            //FieldOfViewControls = {a, b, c};?
            FieldOfViewControls[0] = txtRobotFOV; FieldOfViewControls[1] = RobotFOV1; FieldOfViewControls[2] = RobotFOV2; FieldOfViewControls[3] = RobotFOV3; FieldOfViewControls[4] = RobotFOV4;
            FireWaitControls[0] = txtRobotFireDelay; FireWaitControls[1] = RobotFireDelay1; FireWaitControls[2] = RobotFireDelay2; FireWaitControls[3] = RobotFireDelay3; FireWaitControls[4] = RobotFireDelay4;
            FireWait2Controls[0] = txtRobotFireDelay2; FireWait2Controls[1] = RobotFireDelay21; FireWait2Controls[2] = RobotFireDelay22; FireWait2Controls[3] = RobotFireDelay23; FireWait2Controls[4] = RobotFireDelay24;
            TurnTimeControls[0] = txtRobotTurnSpeed; TurnTimeControls[1] = RobotTurnTime1; TurnTimeControls[2] = RobotTurnTime2; TurnTimeControls[3] = RobotTurnTime3; TurnTimeControls[4] = RobotTurnTime4;
            MaxSpeedControls[0] = txtRobotMaxSpeed; MaxSpeedControls[1] = RobotMaxSpeed1; MaxSpeedControls[2] = RobotMaxSpeed2; MaxSpeedControls[3] = RobotMaxSpeed3; MaxSpeedControls[4] = RobotMaxSpeed4;
            CircleDistControls[0] = txtRobotCircleDist; CircleDistControls[1] = RobotCircleDist1; CircleDistControls[2] = RobotCircleDist2; CircleDistControls[3] = RobotCircleDist3; CircleDistControls[4] = RobotCircleDist4;
            FireCountControls[0] = txtRobotShotCount; FireCountControls[1] = RobotShotCount1; FireCountControls[2] = RobotShotCount2; FireCountControls[3] = RobotShotCount3; FireCountControls[4] = RobotShotCount4;
            EvadeSpeedControls[0] = txtRobotEvadeSpeed; EvadeSpeedControls[1] = RobotEvadeSpeed1; EvadeSpeedControls[2] = RobotEvadeSpeed2; EvadeSpeedControls[3] = RobotEvadeSpeed3; EvadeSpeedControls[4] = RobotEvadeSpeed4;
            //I really hope there's a better way of doing this that isn't so prone to issues
            //I can't believe that a programming language would have a hole in functionality this terrible tbh
            //Or maybe I'm doing something dumb here?

            this.tabPage = tabPage;
            this.transactionManager = transactionManager;
        }

        public void Init(List<VClip> VClips, List<string> SoundNames, List<Robot> Robots, List<Weapon> Weapons, List<Powerup> Powerups, List<Polymodel> Models, bool d1)
        {
            RobotNames = new string[Robots.Count];
            for (int i = 0; i < Robots.Count; i++)
                RobotNames[i] = Robots[i].Name;
            PowerupNames = new string[Powerups.Count];
            for (int i = 0; i < Powerups.Count; i++)
                PowerupNames[i] = Powerups[i].Name;

            cbRobotAttackSound.Items.Clear();
            cbRobotClawSound.Items.Clear();
            cbRobotDyingSound.Items.Clear();
            cbRobotSeeSound.Items.Clear();
            cbRobotTauntSound.Items.Clear();
            cbRobotHitSound.Items.Clear();
            cbRobotDeathSound.Items.Clear();

            string[] stringarray = SoundNames.ToArray();
            cbRobotAttackSound.Items.AddRange(stringarray);
            cbRobotClawSound.Items.AddRange(stringarray);
            cbRobotDyingSound.Items.AddRange(stringarray);
            cbRobotSeeSound.Items.AddRange(stringarray);
            cbRobotTauntSound.Items.AddRange(stringarray);
            cbRobotHitSound.Items.AddRange(stringarray);
            cbRobotDeathSound.Items.AddRange(stringarray);

            cbRobotWeapon1.Items.Clear();
            cbRobotWeapon2.Items.Clear(); cbRobotWeapon2.Items.Add("None");

            stringarray = new string[Weapons.Count];
            for (int i = 0; i < Weapons.Count; i++)
                stringarray[i] = Weapons[i].Name;
            cbRobotWeapon1.Items.AddRange(stringarray);
            cbRobotWeapon2.Items.AddRange(stringarray);

            RobotHitVClipComboBox.Items.Clear(); RobotHitVClipComboBox.Items.Add("None");
            RobotDeathVClipComboBox.Items.Clear(); RobotDeathVClipComboBox.Items.Add("None");

            stringarray = new string[VClips.Count];
            for (int i = 0; i < VClips.Count; i++)
                stringarray[i] = VClips[i].Name;
            RobotHitVClipComboBox.Items.AddRange(stringarray);
            RobotDeathVClipComboBox.Items.AddRange(stringarray);
            RobotModelComboBox.Items.Clear();

            stringarray = new string[Models.Count];
            for (int i = 0; i < Models.Count; i++)
                stringarray[i] = Models[i].Name;
            RobotModelComboBox.Items.AddRange(stringarray);

            cbRobotWeapon2.Visible = !d1;
            cbKamikaze.Visible = !d1;
            txtRobotBadass.Visible = !d1;
            txtRobotEnergyDrain.Visible = !d1;
            cbRobotCompanion.Visible = !d1;
            txtRobotHitBlobs.Visible = !d1;
            txtRobotDeathBlobs.Visible = !d1;
            txtRobotPursuit.Visible = !d1;
            cbRobotThief.Visible = !d1;
            txtRobotLightcast.Visible = !d1;
            txtRobotDeathRolls.Visible = !d1;
            cbRobotTauntSound.Visible = !d1;
            cbRobotDyingSound.Visible = !d1;
            txtRobotGlow.Visible = !d1;
            cbRobotAI.Visible = !d1;
            txtRobotAim.Visible = !d1;

            lbRobotWeapon2.Visible = !d1;
            lbRobotBadass.Visible = !d1;
            lbRobotEnergyDrain.Visible = !d1;
            lbRobotHitBlobs.Visible = !d1;
            lbRobotDeathBlobs.Visible = !d1;
            lbRobotPursuit.Visible = !d1;
            lbRobotLightcast.Visible = !d1;
            lbRobotDeathRolls.Visible = !d1;
            lbRobotTauntSound.Visible = !d1;
            lbRobotDyingSound.Visible = !d1;
            lbRobotGlow.Visible = !d1;
            lbRobotAI.Visible = !d1;
            lbRobotAim.Visible = !d1;
            lbRobotFireDelay2.Visible = !d1;
            for (int i = 0; i < 5; i++)
                FireWait2Controls[i].Visible = !d1;
        }

        public void Init(List<VClip> VClips, List<string> SoundNames, string[] RobotNames, string[] WeaponNames, List<Powerup> Powerups, string[] ModelNames)
        {
            this.RobotNames = RobotNames;
            PowerupNames = new string[Powerups.Count];
            for (int i = 0; i < Powerups.Count; i++)
                PowerupNames[i] = Powerups[i].Name;

            cbRobotAttackSound.Items.Clear();
            cbRobotClawSound.Items.Clear();
            cbRobotDyingSound.Items.Clear();
            cbRobotSeeSound.Items.Clear();
            cbRobotTauntSound.Items.Clear();
            cbRobotHitSound.Items.Clear();
            cbRobotDeathSound.Items.Clear();

            string[] stringarray = SoundNames.ToArray();
            cbRobotAttackSound.Items.AddRange(stringarray);
            cbRobotClawSound.Items.AddRange(stringarray);
            cbRobotDyingSound.Items.AddRange(stringarray);
            cbRobotSeeSound.Items.AddRange(stringarray);
            cbRobotTauntSound.Items.AddRange(stringarray);
            cbRobotHitSound.Items.AddRange(stringarray);
            cbRobotDeathSound.Items.AddRange(stringarray);

            cbRobotWeapon1.Items.Clear();
            cbRobotWeapon2.Items.Clear(); cbRobotWeapon2.Items.Add("None");

            cbRobotWeapon1.Items.AddRange(WeaponNames);
            cbRobotWeapon2.Items.AddRange(WeaponNames);

            RobotHitVClipComboBox.Items.Clear(); RobotHitVClipComboBox.Items.Add("None");
            RobotDeathVClipComboBox.Items.Clear(); RobotDeathVClipComboBox.Items.Add("None");

            stringarray = new string[VClips.Count];
            for (int i = 0; i < VClips.Count; i++)
                stringarray[i] = VClips[i].Name;
            RobotHitVClipComboBox.Items.AddRange(stringarray);
            RobotDeathVClipComboBox.Items.AddRange(stringarray);
            RobotModelComboBox.Items.Clear();

            RobotModelComboBox.Items.AddRange(ModelNames);
        }

        public void InitHXM(EditorHXMFile hxmFile)
        {
            this.hxmFile = hxmFile;
            HXMGroupBox.Visible = true;
        }

        public void ChangeOwnName(string newname)
        {
            RobotNames[robotid] = newname;
            if (robot.ContainsType == 2) //need to update the UI too
            {
                cbRobotDropItem.Items[robotid] = newname;
            }
        }

        //Fillers
        public void Update(Robot robot, int id)
        {
            isLocked = true;
            this.robot = robot;
            this.robotid = id;
            UIUtil.SafeFillComboBox(cbRobotAttackSound, robot.AttackSound);
            UIUtil.SafeFillComboBox(cbRobotClawSound, robot.ClawSound);
            txtRobotDrag.Text = robot.Drag.ToString();
            txtRobotDropProb.Text = robot.ContainsProbability.ToString();
            txtRobotDrops.Text = robot.ContainsCount.ToString();
            txtRobotLight.Text = robot.Lighting.ToString();
            txtRobotMass.Text = robot.Mass.ToString();
            txtRobotScore.Text = robot.ScoreValue.ToString();
            UIUtil.SafeFillComboBox(cbRobotHitSound, robot.HitSoundNum);
            UIUtil.SafeFillComboBox(cbRobotDeathSound, robot.DeathSoundNum);
            UIUtil.SafeFillComboBox(cbRobotSeeSound, robot.SeeSound);
            txtRobotShield.Text = robot.Strength.ToString();
            UIUtil.SafeFillComboBox(cbRobotTauntSound, robot.TauntSound);
            txtRobotAim.Text = robot.Aim.ToString();
            txtRobotBadass.Text = robot.DeathExplosionRadius.ToString();
            txtRobotDeathBlobs.Text = robot.SmartBlobsOnDeath.ToString();
            txtRobotDeathRolls.Text = robot.DeathRollTime.ToString();
            txtRobotEnergyDrain.Text = robot.EnergyDrain.ToString();
            txtRobotHitBlobs.Text = robot.SmartBlobsOnHit.ToString();
            txtRobotGlow.Text = robot.Glow.ToString();
            txtRobotPursuit.Text = robot.Pursuit.ToString();
            cbRobotDyingSound.SelectedIndex = robot.DeathRollSound;
            txtRobotLightcast.Text = robot.LightCast.ToString();

            cbRobotCompanion.Checked = robot.Companion;
            cbRobotClaw.Checked = robot.AttackType == RobotAttackType.Melee ? true : false;
            cbRobotThief.Checked = robot.Thief;
            cbKamikaze.Checked = robot.Kamikaze != 0;

            int dropType = robot.ContainsType;
            if (dropType == 2)
                dropType = 1;
            else
                dropType = 0;

            UpdateRobotDropTypes(dropType, robot);
            RobotDropTypeComboBox.SelectedIndex = dropType;

            UIUtil.SafeFillComboBox(RobotHitVClipComboBox, robot.HitVClipNum + 1);
            UIUtil.SafeFillComboBox(RobotDeathVClipComboBox, robot.DeathVClipNum + 1);
            UIUtil.SafeFillComboBox(RobotModelComboBox, robot.ModelNum);
            UIUtil.SafeFillComboBox(cbRobotWeapon1, robot.WeaponType);
            UIUtil.SafeFillComboBox(cbRobotWeapon2, robot.WeaponTypeSecondary + 1);
            if (robot.Behavior >= RobotAIType.Still)
            {
                cbRobotAI.SelectedIndex = (int)robot.Behavior - 128;
            }
            else cbRobotAI.SelectedIndex = 0;

            RobotBossType bossMode = robot.BossFlag;
            if (bossMode > RobotBossType.RedFatty)
                bossMode -= 18;
            UIUtil.SafeFillComboBox(cmRobotBoss, (int)bossMode);
            UIUtil.SafeFillComboBox(cmRobotCloak, (int)robot.CloakType);

            UpdateRobotAI();
            if (hxmFile != null)
            {
                UpdateRobotAnimation();
            }
            isLocked = false;
        }

        private void UpdateRobotAnimation()
        {
            if (robot.ModelNum >= hxmFile.GetNumModels())
            {
                BaseJointSpinner.Value = 0;
                NumJointsTextBox.Text = "0";
                UnallocatedModelWarning.Visible = true;
            }
            else
            {
                UnallocatedModelWarning.Visible = false;
                if (hxmFile.GetModel(robot.ModelNum).IsAnimated)
                {
                    RobotAnimationCheckbox.Checked = true;
                    BaseJointSpinner.Value = (Decimal)robot.baseJoint;
                }
                else
                    RobotAnimationCheckbox.Checked = false;

                NumJointsTextBox.Text = (Robot.NumAnimationStates * (hxmFile.GetModel(robot.ModelNum).NumSubmodels - 1)).ToString();
            }
        }

        private void UpdateRobotDropTypes(int dropType, Robot robot)
        {
            cbRobotDropItem.Items.Clear();
            if (dropType != 1)
            {
                cbRobotDropItem.Items.AddRange(PowerupNames);
                cbRobotDropItem.SelectedIndex = robot.ContainsID;
            }
            else
            {
                cbRobotDropItem.Items.AddRange(RobotNames);
                cbRobotDropItem.SelectedIndex = robot.ContainsID;
            }
            //cbRobotDropItem.SelectedIndex = 0;
        }

        private void UpdateRobotAI()
        {
            for (int num = 0; num < 5; num++)
            {
                CircleDistControls[num].Text = robot.CircleDistance[num].ToString();
                EvadeSpeedControls[num].Text = robot.EvadeSpeed[num].ToString();
                FireWaitControls[num].Text = robot.FiringWait[num].ToString();
                FireWait2Controls[num].Text = robot.FiringWaitSecondary[num].ToString();
                FieldOfViewControls[num].Text = ((int)(Math.Round(Math.Acos(robot.FieldOfView[num]) * 180 / Math.PI, MidpointRounding.AwayFromZero))).ToString();
                MaxSpeedControls[num].Text = robot.MaxSpeed[num].ToString();
                TurnTimeControls[num].Text = robot.TurnTime[num].ToString();
                FireCountControls[num].Text = robot.RapidfireCount[num].ToString();
            }
        }

        //Updators
        private void RobotComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLocked || transactionManager.TransactionInProgress)
            {
                return;
            }
            ComboBox sendingControl = (ComboBox)sender;
            string tagstr = (string)sendingControl.Tag;
            int value = sendingControl.SelectedIndex;

            IntegerTransaction transaction = new IntegerTransaction("Robot property", robot, (string)sendingControl.Tag, robotid, tabPage, value);
            transactionManager.ApplyTransaction(transaction);

            //[ISB] ugly hack, show new value of joints and animation checkbox
            if (hxmFile != null)
            {
                isLocked = true;
                UpdateRobotAnimation();
                isLocked = false;
            }
        }

        private void RobotProperty_TextChanged(object sender, EventArgs e)
        {
            if (isLocked || transactionManager.TransactionInProgress)
                return;
            TextBox textBox = (TextBox)sender;
            int value;
            if (int.TryParse(textBox.Text, out value))
            {
                IntegerTransaction transaction = new IntegerTransaction("Robot property", robot, (string)textBox.Tag, robotid, tabPage, value);
                transactionManager.ApplyTransaction(transaction);
            }
        }

        private void RobotPropertyFixed_TextChanged(object sender, EventArgs e)
        {
            if (isLocked || transactionManager.TransactionInProgress)
            {
                return;
            }
            TextBox textBox = (TextBox)sender;

            float fvalue;
            if (float.TryParse(textBox.Text, out fvalue))
            {
                FixTransaction transaction = new FixTransaction("Robot property", robot, (string)textBox.Tag, robotid, tabPage, fvalue);
                transactionManager.ApplyTransaction(transaction);
            }
        }

        private void RobotCloak_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLocked)
                return;
            robot.CloakType = (RobotCloakType)cmRobotCloak.SelectedIndex;
        }

        private void RobotBoss_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLocked)
                return;
            int bosstype = cmRobotBoss.SelectedIndex;
            if (bosstype >= 3)
            {
                bosstype += 18;
            }
            //robot.BossFlag = (RobotBossType)bosstype;
            IntegerTransaction transaction = new IntegerTransaction("Robot property", robot, "BossFlag", robotid, tabPage, bosstype);
            transactionManager.ApplyTransaction(transaction);
        }

        private void RobotAI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLocked)
                return;
            int bosstype = cbRobotAI.SelectedIndex;
            //robot.Behavior = (RobotAIType)(bosstype + 0x80);
            IntegerTransaction transaction = new IntegerTransaction("Robot property", robot, "Behavior", robotid, tabPage, bosstype + 0x80);
            transactionManager.ApplyTransaction(transaction);
        }

        private void RobotDropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLocked || transactionManager.TransactionInProgress) return;
            RobotDropTypeTransaction transaction = new RobotDropTypeTransaction("Robot drop type", robotid, tabPage, robot, RobotDropTypeComboBox.SelectedIndex == 1 ? 2 : 7);
            transactionManager.ApplyTransaction(transaction);
            isLocked = true;
            UpdateRobotDropTypes(RobotDropTypeComboBox.SelectedIndex, robot);
            isLocked = false;
        }

        private void RobotCheckBox_CheckedChange(object sender, EventArgs e)
        {
            if (isLocked)
                return;
            CheckBox input = (CheckBox)sender;
            BoolTransaction transaction = new BoolTransaction("Robot flag", robot, (string)input.Tag, robotid, tabPage, input.Checked);
            transactionManager.ApplyTransaction(transaction);
        }

        //HXM editors
        private void BaseJointSpinner_ValueChanged(object sender, EventArgs e)
        {
            if (isLocked)
                return;
            robot.baseJoint = (int)BaseJointSpinner.Value;
        }

        private void RobotAnimationCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (isLocked)
                return;
            if (robot.ModelNum < hxmFile.GetNumModels())
            {
                Polymodel model = hxmFile.GetModel(robot.ModelNum);
                model.IsAnimated = RobotAnimationCheckbox.Checked;
            }
        }

        //I hate everything right now
        //Needed to have values that are -1 the specified value
        private void RobotHitVClipComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLocked || transactionManager.TransactionInProgress)
            {
                return;
            }
            ComboBox sendingControl = (ComboBox)sender;
            string tagstr = (string)sendingControl.Tag;
            int value = sendingControl.SelectedIndex;

            IntegerTransaction transaction = new IntegerTransaction("Robot property", robot, (string)sendingControl.Tag, robotid, tabPage, value-1);
            transactionManager.ApplyTransaction(transaction);
        }

        private void AIFixedProperty_TextChanged(object sender, EventArgs e)
        {
            if (isLocked || transactionManager.TransactionInProgress)
                return;

            TextBox sendingControl = (TextBox)sender;
            string[] tagstrs = ((string)sendingControl.Tag).Split(',');
            string field = tagstrs[0];
            uint index = uint.Parse(tagstrs[1]);

            float fvalue;
            if (float.TryParse(sendingControl.Text, out fvalue))
            {
                if (field == "FieldOfView")
                {
                    fvalue = (float)Math.Cos(fvalue * Math.PI / 180.0D);
                }
                IndexedFixTransaction transaction = new IndexedFixTransaction("Robot AI property", robot, field, robotid, tabPage, index, fvalue);
                transactionManager.ApplyTransaction(transaction);
            }
        }

        private void AIProperty_TextChanged(object sender, EventArgs e)
        {
            if (isLocked || transactionManager.TransactionInProgress)
                return;

            TextBox sendingControl = (TextBox)sender;
            string[] tagstrs = ((string)sendingControl.Tag).Split(',');
            string field = tagstrs[0];
            uint index = uint.Parse(tagstrs[1]);

            int value;
            if (int.TryParse(sendingControl.Text, out value))
            {
                IndexedIntegerTransaction transaction = new IndexedIntegerTransaction("Robot AI property", robot, field, robotid, tabPage, index, value);
                transactionManager.ApplyTransaction(transaction);
            }
        }

        private void RobotCheckBoxInteger_CheckedChanged(object sender, EventArgs e)
        {
            //hack to convert bool input into not bool output
            if (isLocked)
                return;
            CheckBox input = (CheckBox)sender;
            IntegerTransaction transaction = new IntegerTransaction("Robot flag", robot, (string)input.Tag, robotid, tabPage, input.Checked ? 1 : 0);
            transactionManager.ApplyTransaction(transaction);
        }
    }
}
