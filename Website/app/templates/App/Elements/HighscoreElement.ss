<div class="section section--highscore">
    <div class="section_content">
        <div class="highscore_text">
            <div class="textimage_text_content">
                <h2 class="textimage_text_title">$Title</h2>
                $Text
            </div>
        </div>


        <div class="highscore_list">
            <% if $TotalHighScore.Count > 0 %>
                <div class="highscore hourly">
                    <h2 class="highscore_title">Letzte Stunde</h2>
                    <div class="inner_list">
                        <% loop $HourlyHighScore.Limit(5) %>
                            <div class="highscore_entry">
                                <h3 class="highscore_entry_pos">Platz $Pos</h3>
                                <h4 class="highscore_entry_name">$User.Nickname</h4>
                                <div class="highscore_entry_user">
                                    <div class="user_part">$User.SelectedBackDeco.Image</div>
                                    <div class="user_part">$User.SelectedSkinColor.Image</div>
                                    <div class="user_part eyes">$User.SelectedEyes.Image</div>
                                    <div class="user_part">$User.SelectedMouth.Image</div>
                                    <div class="user_part">$User.SelectedHair.Image</div>
                                    <div class="user_part">$User.SelectedBottom.Image</div>
                                    <div class="user_part">$User.SelectedTop.Image</div>
                                    <div class="user_part">$User.SelectedHat.Image</div>
                                </div>
                                <h4 class="highscore_entry_score">$Points Punkte</h4>
                                <h4 class="highscore_entry_date">$Created</h4>
                            </div>
                        <% end_loop %>
                    </div>
                </div>

                <div class="highscore daily">
                    <h2 class="highscore_title">Tag</h2>
                    <div class="inner_list">
                        <% loop $DailyHighScore.Limit(1) %>
                            <div class="highscore_entry">
                                <h3 class="highscore_entry_pos">Platz $Pos</h3>
                                <h4 class="highscore_entry_name">$User.Nickname</h4>
                                <div class="highscore_entry_user">
                                    <div class="user_part">$User.SelectedBackDeco.Image</div>
                                    <div class="user_part">$User.SelectedSkinColor.Image</div>
                                    <div class="user_part eyes">$User.SelectedEyes.Image</div>
                                    <div class="user_part">$User.SelectedMouth.Image</div>
                                    <div class="user_part">$User.SelectedHair.Image</div>
                                    <div class="user_part">$User.SelectedBottom.Image</div>
                                    <div class="user_part">$User.SelectedTop.Image</div>
                                    <div class="user_part">$User.SelectedHat.Image</div>
                                </div>
                                <h4 class="highscore_entry_score">$Points Punkte</h4>
                                <h4 class="highscore_entry_date">$Created</h4>
                            </div>
                        <% end_loop %>
                    </div>
                </div>

                <div class="highscore monthly">
                    <h2 class="highscore_title">Monat</h2>
                    <div class="inner_list">
                        <% loop $MonthlyHighScore.Limit(1) %>
                            <div class="highscore_entry">
                                <h3 class="highscore_entry_pos">Platz $Pos</h3>
                                <h4 class="highscore_entry_name">$User.Nickname</h4>
                                <div class="highscore_entry_user">
                                    <div class="user_part">$User.SelectedBackDeco.Image</div>
                                    <div class="user_part">$User.SelectedSkinColor.Image</div>
                                    <div class="user_part eyes">$User.SelectedEyes.Image</div>
                                    <div class="user_part">$User.SelectedMouth.Image</div>
                                    <div class="user_part">$User.SelectedHair.Image</div>
                                    <div class="user_part">$User.SelectedBottom.Image</div>
                                    <div class="user_part">$User.SelectedTop.Image</div>
                                    <div class="user_part">$User.SelectedHat.Image</div>
                                </div>
                                <h4 class="highscore_entry_score">$Points Punkte</h4>
                                <h4 class="highscore_entry_date">$Created</h4>
                            </div>
                        <% end_loop %>
                    </div>
                </div>

                <div class="highscore total">
                    <h2 class="highscore_title">All Time</h2>
                    <div class="inner_list">
                        <% loop $TotalHighScore.Limit(1) %>
                            <div class="highscore_entry">
                                <h3 class="highscore_entry_pos">Platz $Pos</h3>
                                <h4 class="highscore_entry_name">$User.Nickname</h4>
                                <div class="highscore_entry_user">
                                    <div class="user_part">$User.SelectedBackDeco.Image</div>
                                    <div class="user_part">$User.SelectedSkinColor.Image</div>
                                    <div class="user_part eyes">$User.SelectedEyes.Image</div>
                                    <div class="user_part">$User.SelectedMouth.Image</div>
                                    <div class="user_part">$User.SelectedHair.Image</div>
                                    <div class="user_part">$User.SelectedBottom.Image</div>
                                    <div class="user_part">$User.SelectedTop.Image</div>
                                    <div class="user_part">$User.SelectedHat.Image</div>
                                </div>
                                <h4 class="highscore_entry_score">$Points Punkte</h4>
                                <h4 class="highscore_entry_date">$Created</h4>
                            </div>
                        <% end_loop %>
                    </div>
                </div>
            <% else %>
                <p>Bisher gibt es in diesem Spiel noch keinen Highscore.</p>
            <% end_if %>
        </div>
    </div>
</div>
